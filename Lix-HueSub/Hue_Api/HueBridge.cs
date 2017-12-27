using Hue.Lights;
using Hue.JSON;
using LX29_Hue_Sub.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Hue.HTTP;

namespace Hue
{
    public enum Alerts
    {
        none,
        select
    }

    public enum Brightness : int
    {
        Off = 0,
        Half = 127,
        Full = 255
    }

    public enum Effects
    {
        none,
        colorloop
    }

    public static class HueColorConverter
    {
        //Begrenzungen für Lampen einfügen
        public const double min = 0.000001;

        public static double[] GetXY(Color color)
        {
            double red = color.R / 255.0;
            double green = color.G / 255.0;
            double blue = color.B / 255.0;

            red = (red > 0.04045) ? Math.Pow((red + 0.055) / (1.0 + 0.055), 2.4) : (red / 12.92);
            green = (green > 0.04045) ? Math.Pow((green + 0.055) / (1.0 + 0.055), 2.4) : (green / 12.92);
            blue = (blue > 0.04045) ? Math.Pow((blue + 0.055) / (1.0 + 0.055), 2.4) : (blue / 12.92);

            double X = red * 0.664511 + green * 0.154324 + blue * 0.162028;
            double Y = red * 0.283881 + green * 0.668433 + blue * 0.047685;
            double Z = red * 0.000088 + green * 0.072310 + blue * 0.986039;

            double x = Math.Max(min, Math.Min(1.0, X / (X + Y + Z)));
            if (double.IsNaN(x)) x = min;

            double y = Math.Max(min, Math.Min(1.0, Y / (X + Y + Z)));
            if (double.IsNaN(y)) y = min;

            return new double[] { x, y };
        }

        public static Color RandColor()
        {
            Random rd = new Random();
            return Color.FromArgb(rd.Next(0, 256), rd.Next(0, 256), rd.Next(0, 256));
        }

        public static double[] RandXY()
        {
            return GetXY(RandColor());
        }
    }

    public class HueBridge
    {
        public readonly string IP;
        public UrlProvider Urls;
        private readonly string appname = "LxHueSub";
        private bool IsAuthenticated = false;

        public HueBridge(string ip)
        {
            Urls = new UrlProvider(ip);
            IP = ip;
            // not needed - clock for every 1 sec update status.
            //timer = new Timer(StatusCheckEvent, null, 0, 1000);
        }

        public delegate void PushButtonOnBridgeEvent();

        public event PushButtonOnBridgeEvent PushButtonOnBridge;

        public static ConcurrentDictionary<int, HueLight> Lights { get; private set; }

        public async Task<bool> InitializeRouter()
        {
            try
            {
                if (!string.IsNullOrEmpty(Settings.Default.BridgeApiKey))
                {
                    TryUpdateLights(true);
                    if (IsAuthenticated) return true;
                }
            }
            catch
            {
            }
            return await Register();
        }

        #region SetLightStatus

        public bool SetLightStatus(int lightKey, int brightness, Color color, int transitionTime = 1)
        {
            return SetLightStatus(lightKey, brightness, HueColorConverter.GetXY(color), transitionTime);
        }

        public bool SetLightStatus(int lightKey, Color color, int transitionTime = 1)
        {
            return SetLightStatus(lightKey, HueColorConverter.GetXY(color), transitionTime);
        }

        public bool SetLightStatus(int lightKey, int brightness, int transitionTime = 1)
        {
            try
            {
                if (Lights != null && IsAuthenticated)
                {
                    brightness = LXMath.Constrain(brightness, 0, 255);
                    if (!Lights[lightKey].state.on)
                    {
                        SetLightStatus(lightKey, true);
                    }
                    var json = "{ " + getJsonBri(brightness) + ", \"transitiontime\": " + transitionTime + "}";

                    if (HttpRestHelper.Put(Urls.GetLampUrl(lightKey), json))
                    {
                        Lights[lightKey].state.bri = brightness;
                        return true;
                    }
                    // Lights[lightKey].state.xy = xy;
                }
            }
            catch
            {
            }
            return false;
        }

        public bool SetLightStatus(int lightKey, int brightness, double[] xy, int transitionTime = 1)
        {
            try
            {
                if (Lights != null && IsAuthenticated)
                {
                    brightness = LXMath.Constrain(brightness, 0, 255);
                    if (!Lights[lightKey].state.on)
                    {
                        SetLightStatus(lightKey, true);
                    }
                    var json = "{" + getJsonXY(xy) + "," + getJsonBri(brightness) + ",\"transitiontime\": " + transitionTime + "}";

                    if (HttpRestHelper.Put(Urls.GetLampUrl(lightKey), json))
                    {
                        Lights[lightKey].state.bri = brightness;
                        Lights[lightKey].state.xy = xy;
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public bool SetLightStatus(int lightKey, Alerts alert, int transitionTime = 1)
        {
            try
            {
                if (Lights != null && IsAuthenticated)
                {
                    if (!Lights[lightKey].state.on && alert != Alerts.none)
                    {
                        SetLightStatus(lightKey, true);
                    }

                    var name = Enum.GetName(typeof(Alerts), alert);
                    var json = "{\"alert\": \"" + name + "\", \"transitiontime\": " + transitionTime + "}";

                    if (HttpRestHelper.Put(Urls.GetLampUrl(lightKey), json))
                    {
                        Lights[lightKey].state.alert = name;
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public bool SetLightStatus(int lightKey, Effects effect, int transitionTime = 1)
        {
            try
            {
                if (Lights != null && IsAuthenticated)
                {
                    if (!Lights[lightKey].state.on && effect != Effects.none)
                    {
                        SetLightStatus(lightKey, true);
                    }

                    var name = Enum.GetName(typeof(Effects), effect);
                    var json = "{\"effect\": \"" + name + "\", \"transitiontime\": " + transitionTime + "}";

                    if (HttpRestHelper.Put(Urls.GetLampUrl(lightKey), json))
                    {
                        Lights[lightKey].state.effect = name;
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public bool SetLightStatus(int lightKey, double[] xy, int transitionTime = 1)
        {
            try
            {
                if (Lights != null && IsAuthenticated)
                {
                    var json = "{" + getJsonXY(xy) + ", \"transitiontime\": " + transitionTime + "}";

                    if (HttpRestHelper.Put(Urls.GetLampUrl(lightKey), json))
                    {
                        Lights[lightKey].state.xy = xy;
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public bool SetLightStatus(int lightKey, bool on_Off, int transitionTime = 1)
        {
            try
            {
                if (Lights != null && IsAuthenticated)
                {
                    if (HttpRestHelper.Put(Urls.GetLampUrl(lightKey), "{\"on\": " + on_Off + ", \"transitiontime\": " + transitionTime + "}"))
                    {
                        Lights[lightKey].state.on = on_Off;
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        #endregion SetLightStatus

        public void ToggleLights(bool on_off, int transitionTime = 1)
        {
            if (Lights != null && IsAuthenticated)
            {
                // push PUT request to /api/key/lights/1/state
                foreach (var light in Lights)
                {
                    SetLightStatus(light.Key, on_off, transitionTime);
                    Thread.Sleep(10);
                    //HueLightState.JsonCommand(new HueLightState{ on = true }, new HueLightState() { on = false }));
                }
            }
        }

        public async void TryUpdateLights(bool force = false)
        {
            try
            {
                if (IsAuthenticated || force)
                {
                    var url = Urls.GetStatusUrl();
                    using (var client = new HttpClient())
                    {
                        var statusResponse = await client.GetStringAsync(url);

                        // error response:
                        //[{"error":{"type":1,"address":"/lights","description":"unauthorized user"}}]
                        if (!statusResponse.Contains("unauthorized user"))
                        {
                            ParseLights(statusResponse);
                            //{"lights":{"1":{"state": {"on":true,"bri":219,"hue":33863,"sat":49,"xy":[0.3680,0.3686],"ct":231,"alert":"none","effect":"none","colormode":"ct","reachable":true}, "type": "Extended color light", "name": "Hue Lamp 1", "modelid": "LCT001", "swversion": "65003148", "pointsymbol": { "1":"none", "2":"none", "3":"none", "4":"none", "5":"none", "6":"none", "7":"none", "8":"none" }},"2":{"state": {"on":true,"bri":219,"hue":33863,"sat":49,"xy":[0.3680,0.3686],"ct":231,"alert":"none","effect":"none","colormode":"ct","reachable":true}, "type": "Extended color light", "name": "Hue Lamp 2", "modelid": "LCT001", "swversion": "65003148", "pointsymbol": { "1":"none", "2":"none", "3":"none", "4":"none", "5":"none", "6":"none", "7":"none", "8":"none" }},"3":{"state": {"on":true,"bri":219,"hue":33863,"sat":49,"xy":[0.3680,0.3686],"ct":231,"alert":"none","effect":"none","colormode":"ct","reachable":true}, "type": "Extended color light", "name": "Hue Lamp 3", "modelid": "LCT001", "swversion": "65003148", "pointsymbol": { "1":"none", "2":"none", "3":"none", "4":"none", "5":"none", "6":"none", "7":"none", "8":"none" }}},"groups":{},"config":{"name": "Philips hue","mac": "00:17:88:09:62:40","dhcp": true,"ipaddress": "192.168.0.113","netmask": "255.255.255.0","gateway": "192.168.0.1","proxyaddress": "","proxyport": 0,"UTC": "2012-11-15T03:08:08","whitelist":{"c20aca42279b2898bb1ce2a470da6d64":{"last use date": "2012-11-14T23:41:41","create date": "2012-11-07T03:00:06","name": "Dmitri Sadakov’s iPhone"},"3b268b59109f63d7319c8f9d2a9d2edb":{"last use date": "2012-11-07T04:31:07","create date": "2012-11-07T04:28:27","name": "soapui"},"2cb1ac173bc8aa7f2cae5a073a11fa8f":{"last use date": "2012-11-12T02:40:02","create date": "2012-11-07T04:28:44","name": "soapui"},"26edc9a619306aa4b473ff22165751f":{"last use date": "2012-11-07T03:00:06","create date": "2012-11-07T04:28:45","name": "soapui"},"343855a103b881726d398c68ac6333":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-07T19:22:04","name": "python_hue"},"b7a7e52143446771752ae6e1c69b0a3":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-13T04:31:39","name": "WinHueApp"},"1ec60546129895441850019217b1753f":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-15T01:35:34","name": "winhueapp"},"3fa667052b1747071bc90d137472433":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-15T02:20:50","name": "winhueapp"},"28fd5ecc3add810fa0aaaa41e1db8a7":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-15T02:23:55","name": "winhueapp"},"2c68b67e2d21c1c73e826292701a5eb":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-15T02:25:20","name": "winhueapp"},"15706f6e1d8b9167d32b2822fe99f8b":{"last use date": "2012-11-15T02:31:25","create date": "2012-11-15T02:30:31","name": "winhueapp"},"1db73d762d1d8ea73c14bbda7fac1bb":{"last use date": "2012-11-07T21:56:21","create date": "2012-11-15T03:00:44","name": "winhueapp"},"f86f8213eacc771e26889e19d01083":{"last use date": "2012-11-15T03:08:08","create date": "2012-11-15T03:07:53","name": "winhueapp"}},"swversion": "01003542","swupdate":{"updatestate":0,"url":"","text":"","notify": false},"linkbutton": true,"portalservices": true},"schedules":{}}
                            // /lights: {"1":{"name": "Hue Lamp 1"},"2":{"name": "Hue Lamp 2"},"3":{"name": "Hue Lamp 3"}}
                            IsAuthenticated = true;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private string getJsonBri(int brightness)
        {
            return "\"bri\": " + brightness;
        }

        private string getJsonXY(double[] xy)
        {
            return "\"xy\": [" + LXMath.Constrain(xy[0], 0.0, 1.0).ToString("F4").Replace(",", ".") + "," + LXMath.Constrain(xy[1], 0.0, 1.0).ToString("F4").Replace(",", ".") + "]";
        }

        private void ParseLights(string json)
        {
            var jss = new JavaScriptSerializer();
            var d = jss.Deserialize<dynamic>(json);
            var lights = d["lights"];

            Lights = new ConcurrentDictionary<int, HueLight>();
            foreach (var light in lights)
            {
                int key = int.Parse(light.Key);
                Lights.TryAdd(key, HueLight.Parse(light.Value, key));
            }
        }

        private async Task<bool> Register()
        {
            var retryCount = 0;
            const int retryMax = 60;
            const int pauseMilliseconds = 1000;

            while (retryCount < retryMax) // wait a minute, check each second
            {
                var body = "{\"devicetype\":\"" + appname + "\"}";
                var responseFromServer = await HttpRestHelper.Post(Urls.GetRegisterUrl(), body);

                if (responseFromServer.Contains("link button not pressed"))
                {
                    //responseFromServer = "[{\"error\":{\"type\":7,\"address\":\"/username\",\"description\":\"invalid value, winhueapp, for parameter, username\"}},{\"error\":{\"type\":101,\"address\":\"\",\"description\":\"link button not pressed\"}}]"
                    // link button not pressed, inform on first try only
                    if (retryCount == 0 && PushButtonOnBridge != null)
                        PushButtonOnBridge();

                    Thread.Sleep(pauseMilliseconds); // sleep for a second, then retry
                    retryCount++;
                }
                else
                {
                    dynamic obj = DynamicJsonConverter.Parse(responseFromServer);
                    // sample response: [{"error":{"type":7,"address":"/username","description":"invalid value, WinHueApp, for parameter, username"}},{"success":{"username":"b7a7e52143446771752ae6e1c69b0a3"}}]

                    string key = ((dynamic[])obj)[0].success.username;

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        Settings.Default.BridgeApiKey = key;
                        Settings.Default.Save();

                        TryUpdateLights(true);
                        IsAuthenticated = true;
                        return true;
                    }
                }
            }

            IsAuthenticated = false;
            return false;
        }
    }

    public class HueLightJSON
    {
        public partial class HueLights
        {
            [JsonProperty("lights")]
            public List<Light> Lights { get; set; }
        }

        public partial class Light
        {
            [JsonProperty("modelid")]
            public string Modelid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("state")]
            public State State { get; set; }

            [JsonProperty("swversion")]
            public string Swversion { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("uniqueid")]
            public string Uniqueid { get; set; }
        }

        public partial class State
        {
            [JsonProperty("alert")]
            public string Alert { get; set; }

            [JsonProperty("bri")]
            public long Bri { get; set; }

            [JsonProperty("colormode")]
            public string Colormode { get; set; }

            [JsonProperty("ct")]
            public long Ct { get; set; }

            [JsonProperty("effect")]
            public string Effect { get; set; }

            [JsonProperty("hue")]
            public long Hue { get; set; }

            [JsonProperty("on")]
            public bool On { get; set; }

            [JsonProperty("reachable")]
            public bool Reachable { get; set; }

            [JsonProperty("sat")]
            public long Sat { get; set; }

            [JsonProperty("xy")]
            public List<double> Xy { get; set; }
        }
    }
}