using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LixHueSub
{
    public class SubMessage : IHueEvent
    {
        public int Amount { get; set; }
        public EventType EventType { get; set; }

        public string Message { get; set; }
        public string Name { get; set; }
        public SubType SubType { get; set; }
        public DateTime Time { get; set; }

        public bool Equals(EventType eventType)
        {
            if (EventType == eventType)
            {
                return true;
            }
            else if (eventType == EventType.allsubs)
            {
                switch (EventType)
                {
                    case EventType.sub:
                    case EventType.resub:
                    case EventType.subgift:
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return "\"" + Name + "\" " + Enum.GetName(typeof(EventType), EventType) + " with " + Enum.GetName(typeof(SubType), SubType) + " for " + Amount + "Months! (" +
                DateTime.Now.Subtract(Time).SingleDuration() + " ago)";
        }
    }

    public class TwitchIRC : IDisposable
    {
        private const string ACTION = "ACTION";
        private const string BAN = "@ban-reason=";
        private const string CLEARCHAT = "CLEARCHAT";
        private const string GLOBALUSERSTATE = "GLOBALUSERSTATE";
        private const string NOTICE = "NOTICE";
        private const string PRIVMSG = "PRIVMSG";
        private const string ROOMSTATE = "ROOMSTATE";
        private const string TO = "@ban-duration=";
        private const string USERNOTICE = "USERNOTICE";
        private const string USERSTATE = "USERSTATE";
        private const string WHISPER = "WHISPER";

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0,
                                                          DateTimeKind.Utc);

        public delegate void SubReceived(SubMessage message);

        public event SubReceived OnSubReceived;
        public string Channel
        { get; private set; }
        public IRC_Client.IRC IRC_Client { get; private set; } = null;

        public bool IsConected
        {
            get { return !string.IsNullOrEmpty(Channel); }
        }

        public int MessageCount { get; private set; }
        public List<SubMessage> SubList { get; private set; } = new List<SubMessage>();

        public static DateTime FromMillisecondsSinceUnixEpoch(long milliseconds)
        {
            return UnixEpoch.AddMilliseconds(milliseconds).ToLocalTime();
        }

        public void Connect(string channel)
        {
            Channel = channel;
            MessageCount = 0;
            IRC_Client?.Dispose();
            IRC_Client = new IRC_Client.IRC(channel);
            IRC_Client.ConnectionComplete += IRC_Client_ConnectionComplete;
            IRC_Client.ConnectAsync();
        }

        private void IRC_Client_ConnectionComplete(object sender, EventArgs e)
        {
            IRC_Client.RawMessageRecieved += Irc_RawMessageRecieved;
            IRC_Client.NetworkError += Irc_NetworkError;
        }

        public void Dispose()
        {
            Channel = "";
            if (IRC_Client != null)
            {
                IRC_Client.Dispose();

                IRC_Client.ConnectionComplete -= IRC_Client_ConnectionComplete;
                IRC_Client.RawMessageRecieved -= Irc_RawMessageRecieved;
                IRC_Client.NetworkError -= Irc_NetworkError;

                IRC_Client = null;
            }
        }

        public bool LoadSubCache()
        {
            try
            {
                if (System.IO.File.Exists("subcache.json"))
                {
                    var json = System.IO.File.ReadAllText("subcache.json");
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        System.IO.File.Delete("subcache.json");
                    }
                    else
                    {
                        var list = JsonConvert.DeserializeObject<List<SubMessage>>(json);
                        SubList = list.OrderByDescending(t => t.Time.Ticks).ToList();
                        return true;
                    }
                }
                return false;
            }
            catch { }
            return false;
        }

        private static Dictionary<irc_params, string> ParseParams(string raw, string spliType)
        {
            Dictionary<irc_params, string> parameters = new Dictionary<irc_params, string>();
            raw = raw.Remove(0, 1);
            raw = raw.Remove(raw.IndexOf(spliType)).Trim();
            int idpp = raw.LastIndexOf(":");
            if (idpp > 0)
            {
                raw = raw.Remove(idpp).Trim();
            }
            string[] saParm = raw.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string param in saParm)
            {
                string[] sa = param.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (sa.Length > 0)
                {
                    string sp = sa[0].Replace("-", "_").Trim();
                    if (Enum.TryParse<irc_params>(sp, out irc_params para))
                    {
                        string si = "";
                        if (sa.Length > 1)
                        {
                            si = sa[1].Replace(" ", "\\s").Trim();
                        }
                        parameters.Add(para, si);
                    }
                }
            }
            return parameters;
        }
        public Action<Exception> OnError;
        private void Irc_NetworkError(object sender, Exception e)
        {
            try
            {
                if (IRC_Client != null)
                {
                    OnError?.Invoke(e);
                    Dispose();
                    System.Threading.Thread.Sleep(5000);
                    Connect(Channel);
                }
            }
            catch
            {
            }
        }

        private void Irc_RawMessageRecieved(object sender, IRC_Client.Events.RawMessageEventArgs e)
        {
            string raw = e.Message;
            if (raw.ContainsAny(out string spliType, false, PRIVMSG, USERNOTICE))
            {
                MessageCount++;
                if (spliType.Equals(USERNOTICE))
                {
                    var channelName = raw.GetBetween(spliType + " #", " :");
                    var parameters = ParseParams(raw, spliType);
                    var Name = raw.GetBetween("login=", ";");
                    var Message = raw.Substring("#" + channelName + " :");

                    var eventType = EventType.none;
                    var months = 0;
                    var subType = SubType.NoSub;
                    var time = DateTime.Now;

                    if (parameters.ContainsKey(irc_params.tmi_sent_ts))
                    {
                        if (long.TryParse(parameters[irc_params.tmi_sent_ts], out long timeSeconds))
                        {
                            time = FromMillisecondsSinceUnixEpoch(timeSeconds);
                        }
                    }

                    if (parameters.ContainsKey(irc_params.msg_id))
                    {
                        //subgift == MSG_ID Subgift
                        Enum.TryParse(parameters[irc_params.msg_id], out eventType);
                    }
                    if (parameters.ContainsKey(irc_params.msg_param_months))
                    {
                        int.TryParse(parameters[irc_params.msg_param_months], out months);
                    }
                    if (parameters.ContainsKey(irc_params.msg_param_sub_plan))
                    {
                        Enum.TryParse(parameters[irc_params.msg_param_sub_plan], out subType);
                    }

                    var subMessage = new SubMessage()
                    { Name = Name, Message = Message, Amount = months, SubType = subType, Time = time, EventType = eventType };
                    SubList.Add(subMessage);

                    OnSubReceived?.Invoke(subMessage);
                }
            }
        }
    }
}