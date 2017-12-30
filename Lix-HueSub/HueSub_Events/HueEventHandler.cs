using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpHue.Config;
using SharpHue;
using LixHueSub.Properties;
using LixHueSub.Tipeee;

namespace LixHueSub
{
    public class HueEventHandler : IDisposable
    {
        public const int LightCount = 1;

        private Queue<Action> queue = new Queue<Action>();

        private LXTimer timer = null;

        public delegate void SubReceived(SubMessage message);

        public event SubReceived OnSubReceived;

        //public HueBridge Bridge { get; private set; }

        public static LightCollection Lights { get; set; }
        public List<HueEvent> Events { get; private set; } = new List<HueEvent>();
        public TwitchIRC IRC { get; private set; } = new TwitchIRC();

        public TipeeEventHandler Tipeee { get; private set; } = new TipeeEventHandler();

        public bool IsChatConected
        {
            get { return IRC.IsConected; }
        }

        public void Dispose()
        {
            IRC?.Dispose();
            Tipeee?.Dispose();
        }

        public Task<Exception> FindBridge(string ip)
        {
            return Task.Run<Exception>(() =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Settings.Default.BridgeApiKey))
                    {
                        SharpHue.Configuration.AddUser(ip);
                        Settings.Default.BridgeApiKey = SharpHue.Configuration.Username;
                        Settings.Default.Save();
                    }
                    else
                    {
                        SharpHue.Configuration.Initialize(Settings.Default.BridgeApiKey, ip);
                    }
                    Lights = new LightCollection();
                    return null;
                }
                catch (Exception x)
                {
                    return x;
                }
                //var b = HueBridgeLocator.Locate(ip).Result;
                //if (b != null)
                //{
                //    Bridge = b;
                //    return true;
                //}
                //return false;
            });
        }
        //public delegate void EventFired(HueEventStatus status);
        //public event EventFired OnEventFired;

        public void FireEvent(HueEvent @event)
        {
            try
            {
                Lights.Refresh();

                var oldLightStates = Lights.Select((t) => new
                {
                    on = t.State.IsOn,
                    col = t.State.HSBColor,
                    key = t.ID
                }).ToList();

                foreach (var stat in @event.States)
                {
                    if (stat.TimerType == TimerType.None)
                    {
                        var newstate = new LightStateBuilder()
                            .Brightness(stat.Brightness)
                            .Color(stat.Color)
                            .TransitionTime(stat.Transition)
                            .Turn(stat.TurnOn)
                            .For(Lights[stat.LightKey]);

                        newstate.Apply();

                        //OnEventFired?.Invoke(stat);

                        if (stat.Duration > 0)
                        {
                            System.Threading.Thread.Sleep((int)stat.Duration);
                        }
                    }
                    else
                    {
                        var watch = new System.Diagnostics.Stopwatch();
                        bool switchOn = !Lights[stat.LightKey].State.IsOn;

                        if (stat.TimerType == TimerType.Color_Switch)
                        {
                            new LightStateBuilder()
                                    .Turn(true)
                                    .For(Lights[stat.LightKey])
                                    .Apply();
                        }

                        new LightStateBuilder()
                            .TransitionTime(stat.Transition)
                            .For(Lights[stat.LightKey])
                            .Apply();

                        for (int i = 0; i < UInt16.MaxValue; i++)
                        {
                            if (watch.ElapsedMilliseconds > stat.Duration) break;

                            if (stat.TimerType == TimerType.On_Off)
                            {
                                new LightStateBuilder()
                                .Turn(switchOn)
                                .For(Lights[stat.LightKey])
                                .Apply();
                            }

                            if (switchOn)
                            {
                                new LightStateBuilder()
                                .Brightness(stat.Brightness)
                                .Color(stat.Color)
                                .For(Lights[stat.LightKey])
                                .Apply();
                            }
                            else if (stat.TimerType == TimerType.Color_Switch)
                            {
                                new LightStateBuilder()
                                .Brightness(stat.Brightness)
                                .Color(stat.Color2)
                                .For(Lights[stat.LightKey])
                                .Apply();
                            }

                            switchOn = !switchOn;

                            System.Threading.Thread.Sleep((int)stat.TimerInterval);

                            if (!watch.IsRunning) watch.Start();
                        }
                    }
                }

                foreach (var light in oldLightStates)
                {
                        new LightStateBuilder()
                            .Turn(light.on)
                            .For(Lights[light.key])
                            .Apply();

                        new LightStateBuilder()
                            .HSBColor(light.col)
                            .For(Lights[light.key])
                            .Apply();
                }
            }
            catch (Exception ae)
            {
                ae.Handle("", true);
            }
        }


        public void LoadEvents(ListBox listBox_ChatEvents, ListBox listBox_HueEvents)
        {
            if (IRC.LoadSubCache())
            {
                listBox_ChatEvents.BeginUpdate();
                listBox_ChatEvents.Items.AddRange(IRC.SubList.ToArray());
                listBox_ChatEvents.EndUpdate();
                listBox_ChatEvents.Refresh();
            }
            if (LoadHueEvents())
            {
                listBox_HueEvents.Items.Clear();
                //listBox_Events.BeginUpdate();
                foreach (var eve in Events)
                {
                    listBox_HueEvents.Items.Add(eve);
                }
                //listBox_Events.EndUpdate();
                listBox_HueEvents.Refresh();
            }
        }

        public void LoginChat(Action listBox)
        {
            TwitchApi.Authenticate((t) =>
            {
                IRC.OnSubReceived += Irc_OnSubReceived1;
#if DEBUG
                IRC.Connect("summit1g");// TwitchApi.Name);
#else
                IRC.Connect(TwitchApi.Name);
#endif
                IRC.IRC_Client.ConnectionComplete += (o, e) =>
                {
                    listBox?.Invoke();
                    timer = new LXTimer(WorkQueue, 100, LXTimer.Infinite);
                };
            });
        }

        public void Save()
        {
            try
            {
                System.IO.File.WriteAllText("HueEvents.json", Newtonsoft.Json.JsonConvert.SerializeObject(Events));
                if (IRC != null)
                {
                    System.IO.File.WriteAllText("subcache.json", Newtonsoft.Json.JsonConvert.SerializeObject(IRC.SubList));
                }
            }
            catch { }
        }

        private void Irc_OnSubReceived1(SubMessage message)
        {
            try
            {
                foreach (var eve in Events)
                {
                    if (eve.Check(message))
                    {
                        Action t = new Action(() => FireEvent(eve));
                        queue.Enqueue(t);
                    }
                }
            }
            catch
            {

            }
            OnSubReceived?.Invoke(message);
        }

        private bool LoadHueEvents()
        {
            try
            {
                if (System.IO.File.Exists("HueEvents.json"))
                {
                    var json = System.IO.File.ReadAllText("HueEvents.json");
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        System.IO.File.Delete("HueEvents.json");
                    }
                    else
                    {
                        var list = JsonConvert.DeserializeObject<List<HueEvent>>(json,
                            new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore });
                        Events = list.OrderByDescending(t => (int)t.EventType).ToList();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception x) { x.Handle("", true); }
            return false;
        }

        private void WorkQueue(LXTimer timer)
        {
            if (queue.Count > 0)
            {
                while (queue.Count > 0)
                {
                    queue.Dequeue().Invoke();
                }
                timer.Change(100, LXTimer.Infinite);
            }
            else
            {
                timer.Change(500, LXTimer.Infinite);
            }
        }
    }
}