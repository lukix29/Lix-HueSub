﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public static class LX29_Tools
    {
        private static System.Text.RegularExpressions.Regex reg =
            new System.Text.RegularExpressions.Regex(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$",
                                System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        public static bool HasInternetConnection
        {
            // There is no way you can reliably check if there is an internet connection, but we can come close
            get
            {
                bool result = false;

                try
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (Ping p = new Ping())
                        {
                            result = (p.Send("8.8.8.8", 1000).Status == IPStatus.Success) || (p.Send("8.8.4.4", 1000).Status == IPStatus.Success) || (p.Send("4.2.2.1", 1000).Status == IPStatus.Success);
                        }
                    }
                }
                catch { }

                return result;
            }
        }

        public static List<string> StartupParameters
        {
            get
            {
                try
                {
                    List<string> startup_parameters_mixed = new List<string>();
                    startup_parameters_mixed.AddRange(Environment.GetCommandLineArgs());

                    List<string> startup_parameters_lower = new List<string>();
                    foreach (string s in startup_parameters_mixed)
                        startup_parameters_lower.Add(s.Trim().ToLower());

                    startup_parameters_mixed.Clear();

                    return startup_parameters_lower;
                }
                catch
                {
                    try { return new List<string>(Environment.GetCommandLineArgs()); }
                    catch { }
                }

                return new List<string>();
            }
        }

        public static void ExecuteIEnumerable<T>(IEnumerable<T> rest, Action<T, int> action)
        {
            int cnt = 0;
            List<Task> tasks = new List<Task>();
            foreach (var r in rest)
            {
                tasks.Add(Task.Run(() => action(r, cnt)));
                if (tasks.Count >= 4)
                {
                    int index = Task.WaitAny(tasks.ToArray());
                    tasks.RemoveAt(index);
                }
                cnt++;
            }
            if (tasks.Count > 0)
            {
                Task.WaitAll(tasks.ToArray());
                tasks.Clear();
            }
        }

        public static Dictionary<string, string> GetInstalledBrowsers()
        {
            Dictionary<string, string> browsers = new Dictionary<string, string>();
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet"))
            {
                var sub = key.GetSubKeyNames();
                foreach (var s in sub)
                {
                    using (RegistryKey browser = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Clients\\StartMenuInternet\\" + s + "\\DefaultIcon"))
                    {
                        var value = browser.GetValue("").ToString();
                        value = value.Remove(value.LastIndexOf(','));

                        var file = System.IO.Path.GetFileNameWithoutExtension(s).ToLower();

                        browsers.Add(file, value);
                    }
                }
            }
            return browsers;
        }

        public static string GetSystemDefaultBrowser()
        {
            string name = string.Empty;
            RegistryKey regKey = null;

            try
            {
                regKey = Registry.ClassesRoot.OpenSubKey("HTTP\\shell\\open\\command", false);

                name = regKey.GetValue(null).ToString().ToLower().Replace("" + (char)34, "");

                if (!name.EndsWith("exe"))
                    name = name.Substring(0, name.LastIndexOf(".exe") + 4);
            }
            catch
            {
            }
            finally
            {
                if (regKey != null)
                    regKey.Close();
            }
            return name;
        }

#if Chatclient
        public static string GetUniqueHash()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
            ManagementObjectCollection moc = MOS.Get();
            string s = "";
            foreach (ManagementObject mo in moc)
            {
                s += mo["UUID"].ToString();
            }
            MOS = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            moc = MOS.Get();
            foreach (ManagementObject mo in moc)
            {
                s += mo["ProcessorId"].ToString();
            }
            s += Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows_NT\\CurrentVersion", "ProductId", "FAIL");

            byte[] ba = Encoding.UTF8.GetBytes(s);
            ba = SHA512Managed.Create().ComputeHash(ba);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ba.Length; i++)
            {
                result.Append(ba[i].ToString("X2"));
            }
            return result.ToString();
        }
#endif

        public static bool IsLink(string input)
        {
            return (input.Contains(".") && !input.Contains("..")
                            && reg.IsMatch(input));
        }
    }

    public class DebugWatch
    {
        private System.Text.StringBuilder result = new System.Text.StringBuilder();
        private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        public static long Frequency
        {
            get { return System.Diagnostics.Stopwatch.Frequency; }
        }

        public static bool IsHighResolution
        {
            get { return System.Diagnostics.Stopwatch.IsHighResolution; }
        }

        public long ElapsedMilliseconds
        {
            get { return watch.ElapsedMilliseconds; }
        }

        public long ElapsedTicks
        {
            get { return watch.ElapsedTicks; }
        }

        public string Results
        {
            get { return result.ToString(); }
        }

        public void Restart()
        {
            watch.Restart();
        }

        public void Restart(string info, bool restart = true)
        {
            result.AppendLine(watch.ElapsedMilliseconds + info);
            if (restart) watch.Restart();
        }

        public void ShowResults()
        {
            System.Windows.Forms.MessageBox.Show(result.ToString());
            watch.Stop();
        }

        public void Start()
        {
            result.Clear();
            watch.Restart();
        }

        public void Stop()
        {
            watch.Stop();
        }
    }

    public class LXTimer : IDisposable
    {
        public const int Infinite = -1;

        /// <summary>
        ///     Initialisiert eine neue Instanz der LXTimer-Klasse zum angegebenen Zeitintervall und startet diesen.
        ///     (Threading.Timer Wrapper)
        /// </summary>
        /// <param name="action">Eine System.Action mit dem Timer als Parameter, der die auszuführende Methode darstellt.</param>
        /// <param name="dueTime">Die in Millisekunden angegebene Zeitspanne, die gewartet werden soll, bis callback aufgerufen wird. Geben Sie System.Threading.Timeout.Infinite(-1) an, um das Starten des Zeitgebers zu verhindern. Geben Sie 0 (null) an, um den Zeitgeber sofort zu starten.</param>
        /// <param name="interval"> Das in Millisekunden angegebene Zeitintervall zwischen den Aufrufen von callback. Geben Sie System.Threading.Timeout.Infinite(-1) an, um periodisches Signalisieren zu deaktivieren.</param>
        public LXTimer(Action<LXTimer> action, int dueTime, int interval)
        {
            Action = action;
            timer = new TTData();
            var t =
                 new System.Threading.Timer(
                     new TimerCallback((ob) => { Action.Invoke((LXTimer)ob); }),
                     this, dueTime, interval);
            timer.timer = t;
        }

        public Action<LXTimer> Action
        {
            get;
            private set;
        }

#pragma warning disable IDE1006 // Benennungsstile

        private TTData timer
#pragma warning restore IDE1006 // Benennungsstile
        {
            get;
            set;
        }

        public void Change(int dueTime, int signalTime)
        {
            timer.timer.Change(dueTime, signalTime);
        }

        public void Dispose(bool b)
        {
            if (b) Dispose();
        }

        public void Dispose()
        {
            if (timer.timer != null)
            {
                timer.timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                timer.timer.Dispose();
            }
        }

        public void Invoke()
        {
            Action.Invoke(this);
        }

        public class TTData
        {
#pragma warning disable IDE1006 // Benennungsstile

            public System.Threading.Timer timer
#pragma warning restore IDE1006 // Benennungsstile
            {
                get;
                set;
            }
        }
    }
}