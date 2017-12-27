using Newtonsoft.Json;
using System;

//namespace Hue
//{
//    public partial class Body
//    {
//        [JsonProperty("bri")]
//        public object Bri { get; set; }

//        [JsonProperty("on")]
//        public bool On { get; set; }

//        [JsonProperty("scene")]
//        public object Scene { get; set; }

//        [JsonProperty("transitiontime")]
//        public object Transitiontime { get; set; }

//        [JsonProperty("xy")]
//        public object Xy { get; set; }
//    }

//    public partial class Command
//    {
//        [JsonProperty("address")]
//        public string Address { get; set; }

//        [JsonProperty("body")]
//        public Body Body { get; set; }

//        [JsonProperty("method")]
//        public string Method { get; set; }
//    }

//    public partial class Config
//    {
//        [JsonProperty("dhcp")]
//        public bool Dhcp { get; set; }

//        [JsonProperty("gateway")]
//        public string Gateway { get; set; }

//        [JsonProperty("ipaddress")]
//        public string Ipaddress { get; set; }

//        [JsonProperty("linkbutton")]
//        public bool Linkbutton { get; set; }

//        [JsonProperty("mac")]
//        public string Mac { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("netmask")]
//        public string Netmask { get; set; }

//        [JsonProperty("portalservices")]
//        public bool Portalservices { get; set; }

//        [JsonProperty("proxyaddress")]
//        public string Proxyaddress { get; set; }

//        [JsonProperty("proxyport")]
//        public long Proxyport { get; set; }

//        [JsonProperty("swupdate")]
//        public Swupdate Swupdate { get; set; }

//        [JsonProperty("swversion")]
//        public string Swversion { get; set; }

//        [JsonProperty("UTC")]
//        public string Utc { get; set; }
//    }

//    public partial class Fluffy1
//    {
//        [JsonProperty("modelid")]
//        public string Modelid { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("state")]
//        public PurpleState State { get; set; }

//        [JsonProperty("swversion")]
//        public string Swversion { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }

//        [JsonProperty("uniqueid")]
//        public string Uniqueid { get; set; }
//    }

//    public partial class FluffyState
//    {
//        [JsonProperty("alert")]
//        public string Alert { get; set; }

//        [JsonProperty("bri")]
//        public long Bri { get; set; }

//        [JsonProperty("colormode")]
//        public string Colormode { get; set; }

//        [JsonProperty("ct")]
//        public long Ct { get; set; }

//        [JsonProperty("effect")]
//        public string Effect { get; set; }

//        [JsonProperty("hue")]
//        public long Hue { get; set; }

//        [JsonProperty("on")]
//        public bool On { get; set; }

//        [JsonProperty("reachable")]
//        public bool? Reachable { get; set; }

//        [JsonProperty("sat")]
//        public long Sat { get; set; }

//        [JsonProperty("xy")]
//        public List<double> Xy { get; set; }
//    }

//    public partial class Groups
//    {
//        [JsonProperty("1")]
//        public Tentacled1 The1 { get; set; }
//    }

//    public partial class HueLights
//    {
//        [JsonProperty("config")]
//        public Config Config { get; set; }

//        [JsonProperty("groups")]
//        public Groups Groups { get; set; }

//        [JsonProperty("lights")]
//        public Lights Lights { get; set; }

//        [JsonProperty("schedules")]
//        public Schedules Schedules { get; set; }
//    }

//    public partial class Lights
//    {
//        [JsonProperty("1")]
//        public Fluffy1 The1 { get; set; }

//        [JsonProperty("2")]
//        public The2 The2 { get; set; }

//        [JsonProperty("3")]
//        public The2 The3 { get; set; }
//    }

//    public partial class Purple1
//    {
//        [JsonProperty("command")]
//        public Command Command { get; set; }

//        [JsonProperty("description")]
//        public string Description { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("time")]
//        public string Time { get; set; }
//    }

//    public partial class PurpleState
//    {
//        [JsonProperty("alert")]
//        public string Alert { get; set; }

//        [JsonProperty("bri")]
//        public long Bri { get; set; }

//        [JsonProperty("colormode")]
//        public string Colormode { get; set; }

//        [JsonProperty("ct")]
//        public long Ct { get; set; }

//        [JsonProperty("effect")]
//        public string Effect { get; set; }

//        [JsonProperty("hue")]
//        public long Hue { get; set; }

//        [JsonProperty("on")]
//        public bool On { get; set; }

//        [JsonProperty("reachable")]
//        public bool Reachable { get; set; }

//        [JsonProperty("sat")]
//        public long Sat { get; set; }

//        [JsonProperty("xy")]
//        public List<long> Xy { get; set; }
//    }

//    public partial class Schedules
//    {
//        [JsonProperty("1")]
//        public Purple1 The1 { get; set; }
//    }

//    public partial class Swupdate
//    {
//        [JsonProperty("notify")]
//        public bool Notify { get; set; }

//        [JsonProperty("text")]
//        public string Text { get; set; }

//        [JsonProperty("updatestate")]
//        public long Updatestate { get; set; }

//        [JsonProperty("url")]
//        public string Url { get; set; }
//    }

//    public partial class Tentacled1
//    {
//        [JsonProperty("action")]
//        public FluffyState Action { get; set; }

//        [JsonProperty("lights")]
//        public List<string> Lights { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }
//    }

//    public partial class The2
//    {
//        [JsonProperty("modelid")]
//        public string Modelid { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("state")]
//        public FluffyState State { get; set; }

//        [JsonProperty("swversion")]
//        public string Swversion { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }

//        [JsonProperty("uniqueid")]
//        public string Uniqueid { get; set; }
//    }
//}

namespace Hue.Lights
{
    public class HueLight : ICloneable
    {
        public int Key { get; set; }
        public string modelid { get; set; }
        public string name { get; set; }
        public HueLightState state { get; set; }
        public long swversion { get; set; }
        public string type { get; set; }

        public static HueLight Parse(dynamic d, int key)
        {
            var instance = new HueLight();

            instance.Key = key;
            instance.state = HueLightState.Parse(d["state"]);
            instance.type = d["type"];
            instance.name = d["name"];
            instance.modelid = d["modelid"];
            instance.swversion = long.Parse(d["swversion"]);

            return instance;
        }

        public object Clone()
        {
            return new HueLight()
            {
                Key = Key,
                modelid = modelid,
                name = name,
                state = (HueLightState)state.Clone(),
                swversion = swversion,
                type = type
            };
        }

        public override string ToString()
        {
            return "LightKey: " + Key + " | " + name + "\r\n(" + type + ")";
        }
    }

    public class HueLightState : ICloneable
    {
        public string alert { get; set; }

        public int bri { get; set; }

        public string colormode { get; set; }

        public int ct { get; set; }

        public string effect { get; set; }

        public int hue { get; set; }

        [JsonIgnore]
        public string JsonCommand
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this,
                    new JsonSerializerSettings()
                    { NullValueHandling = NullValueHandling.Ignore, FloatFormatHandling = FloatFormatHandling.DefaultValue });// JsonDiff(new HueLightState());
            }
        }

        public bool on { get; set; }

        public bool reachable { get; set; }

        public int sat { get; set; }

        public double[] xy { get; set; }

        public static HueLightState Parse(dynamic d)
        {
            var instance = new HueLightState();
            try
            {
                instance.on = d["on"];
                instance.bri = d["bri"];
                instance.hue = d["hue"];
                instance.sat = d["sat"];
                instance.xy = new double[] { (double)d["xy"][0], (double)d["xy"][1] };
                instance.alert = d["alert"];
                instance.effect = d["effect"];
                instance.colormode = d["colormode"];
                instance.reachable = d["reachable"];
            }
            catch
            {
            }
            return instance;
        }

        public object Clone()
        {
            return new HueLightState()
            {
                alert = this.alert,
                bri = this.bri,
                colormode = this.colormode,
                ct = this.ct,
                effect = this.effect,
                hue = this.hue,
                on = this.on,
                reachable = this.reachable,
                sat = this.sat,
                xy = this.xy
            };
        }

        //public string DiffJson(float[] XY)
        //{
        //    var state = (HueLightState)Clone();
        //    state.xy = XY;
        //    return DiffJson(state);
        //}

        //public string DiffJson(HueLightState newState)
        //{
        //    //stream
        //    //using (var json = new JsonTextWriter())
        //    //{
        //    //}
        //    StringBuilder stringBuilder = new StringBuilder();
        //    //  JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        //    stringBuilder.Append(getDiff(newState, "alert"));
        //    stringBuilder.Append(getDiff(newState, "bri"));
        //    stringBuilder.Append(getDiff(newState, "colormode"));
        //    stringBuilder.Append(getDiff(newState, "ct"));
        //    stringBuilder.Append(getDiff(newState, "hue"));
        //    stringBuilder.Append(getDiff(newState, "on"));
        //    stringBuilder.Append(getDiff(newState, "reachable"));
        //    stringBuilder.Append(getDiff(newState, "sat"));
        //    stringBuilder.Append(getDiff(newState, "xy"));

        //    //if (newState.alert != this.alert)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.alert, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.bri != this.bri)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.bri, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.colormode != this.colormode)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.colormode, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.ct != this.ct)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.ct, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.effect != this.effect)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.effect, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.hue != this.hue)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.hue, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.on != this.on)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.on, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.reachable != this.reachable)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.reachable, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.sat != this.sat)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.sat, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    //if (newState.xy != this.xy)
        //    //{
        //    //    var json = JsonConvert.SerializeObject(newState.xy, settings);
        //    //    stringBuilder.Append(json + ",");
        //    //}
        //    var output = stringBuilder.ToString();
        //    output = "{" + output.Trim(',') + "}";
        //    return output;
        //}

        //public void Update(HueLightState state)
        //{
        //    if (alert != state.alert)
        //    {
        //        alert = state.alert;
        //    }

        //    if (bri != state.bri)
        //    {
        //        bri = state.bri;
        //    }

        //    if (alert != state.alert)
        //    {
        //        colormode = state.colormode;
        //    }

        //    if (colormode != state.colormode)
        //    {
        //        ct = state.ct;
        //    }

        //    if (effect != state.effect)
        //    {
        //        effect = state.effect;
        //    }

        //    if (hue != state.hue)
        //    {
        //        hue = state.hue;
        //    }

        //    if (on != state.on)
        //    {
        //        on = state.on;
        //    }

        //    if (reachable != state.reachable)
        //    {
        //        reachable = state.reachable;
        //    }

        //    if (sat != state.sat)
        //    {
        //        sat = state.sat;
        //    }

        //    if (xy != state.xy)
        //    {
        //        xy = state.xy;
        //    }
        //}

        //private string getDiff(HueLightState newState, string name)
        //{
        //    try
        //    {
        //        PropertyInfo propertyInfo = newState.GetType().GetProperty(name);
        //        PropertyInfo propertyInfoOld = this.GetType().GetProperty(name);
        //        var prop1 = propertyInfo.GetValue(newState);
        //        var prop2 = propertyInfoOld.GetValue(this);
        //        if (prop1 != null && prop2 != null)
        //        {
        //            if (!prop1.Equals(prop2))
        //            {
        //                var json = "\"" + name + "\":" + prop1.ToString() + ",";// JsonConvert.SerializeObject(newState.alert, settings);
        //                return json;
        //            }
        //        }
        //    }
        //    catch { }
        //    return string.Empty;
        //}
    }
}