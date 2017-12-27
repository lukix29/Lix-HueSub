using LX29_Hue_Sub.Properties;
using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Hue
{
    /// <summary>
    /// Locates the Philips Hue lights bridge using SSDP
    /// </summary>
    public class HueBridgeLocator
    {
        public delegate void UpdateInfo(string info);

        public static event UpdateInfo OnUpdatedInfo;

        public async static Task<HueBridge> Locate(string ipadress)
        {
            try
            {
                var localAdress = GetLocalIPAddress();
                string ipBase = localAdress.ToString();
                ipBase = ipBase.Remove(ipBase.LastIndexOf('.') + 1);

                ipadress = (string.IsNullOrWhiteSpace(ipadress)) ? Settings.Default.LastIP : ipadress;

                if (!String.IsNullOrEmpty(ipadress))
                {
                    var br = await Task.Run<HueBridge>(() =>
                     {
                         return PingFindBridge(ipadress);
                     });
                    if (br != null)
                    {
                        return br;
                    }
                }
                //ConcurrentBag<IPAddress> list = new ConcurrentBag<IPAddress>();

                return await Task.Run(() =>
                 {
                     HueBridge hueBridge = null;
                     Parallel.For(2, 256, (i, state) =>
                      {
                          string ip = ipBase + i.ToString();

                          var bridge = PingFindBridge(ip);
                          if (bridge != null)
                          {
                              if (hueBridge == null)
                              {
                                  hueBridge = bridge;
                              }
                              state.Stop();
                          }
                      });
                     return hueBridge;
                 });
            }
            catch
            {
            }
            return null;
        }

        private static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        // http://www.nerdblog.com/2012/10/a-day-with-philips-hue.html - description.xml retrieval
        private async static Task<bool> IsHue(string discoveryUrl)
        {
            using (var http = new HttpClient { Timeout = TimeSpan.FromMilliseconds(2000) })
            {
                try
                {
                    var res = await http.GetStringAsync(discoveryUrl);
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        if (res.Contains("Philips hue"))
                            return true;
                    }
                }
                catch
                {
                    return false;
                }
                return false;
            }
        }

        private static HueBridge PingFindBridge(string ip)
        {
            using (Ping p = new Ping())
            {
                var e = p.Send(ip, 50);

                OnUpdatedInfo?.Invoke(ip);

                if (e.Status == IPStatus.Success)
                {
                    var end = "http://" + ip + "/api/" +
                    ((String.IsNullOrEmpty(Settings.Default.BridgeApiKey)) ? "newdeveloper" : Settings.Default.BridgeApiKey);

                    if (IsHue(end).Result)
                    {
                        return new HueBridge(ip.ToString());
                    }
                }
            }
            return null;
        }
    }
}