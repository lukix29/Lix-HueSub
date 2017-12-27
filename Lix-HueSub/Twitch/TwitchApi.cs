using LixHueSub.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace LixHueSub
{
    public enum irc_params
    {
        color,
        badges,
        emotes,
        display_name,
        mod,
        subscriber,
        tmi_sent_ts,
        turbo,
        user_type,
        ban_duration,
        ban_reason,
        room_id,
        user_id,
        target_user_id,
        msg_param_months,
        system_msg,
        msg_id,
        id,
        msg_param_sub_plan,
        msg_param_sub_plan_name
    }

    public enum SubType : int
    {
        NoSubProgram = -2000,
        NoSub = -1000,
        Prime = 0000,
        Tier1 = 1000,
        Tier2 = 2000,
        Tier3 = 3000
    }

    public class TwitchApi
    {
        private const string ClientID = "xgo1g2cja2aafpn5vro0sw8twvkuqo";

        private const string loginUrl = "https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id="
                   + ClientID + "&redirect_uri=http://localhost:12685&scope=chat_login+user_read&force_verify=true";

        public static int ID { get; set; }

        public static bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(Token); }
        }

        public static string Name { get; set; } = "";

        public static string Token
        {
            get { return Settings.Default.ChatToken; }
        }

        public static void Authenticate(Action<ApiResult.TokenResult> action)
        {
            if (!string.IsNullOrWhiteSpace(Token) &&
               DateTime.Now.Subtract(Settings.Default.ChatTokenValidity).TotalSeconds < 0)
            {
                var t = GetApiResult();

                action?.Invoke(t);
            }
            else
            {
                Token_HTTP_Server server = new Token_HTTP_Server(12685);
                server.ReceivedToken += (Token) =>
                {
                    Settings.Default.ChatToken = Token;

                    var t = GetApiResult();

                    Settings.Default.ChatTokenValidity = t.token.Authorization.CreatedAt.AddSeconds(t.token.ExpiresIn);
                    Settings.Default.Save();

                    action?.Invoke(t);
                };
                server.Start();
                System.Diagnostics.Process.Start(loginUrl);
            }
        }

        public static ApiResult.TokenResult GetApiResult()
        {
            using (WebClient webclient = new WebClient())
            {
                webclient.Proxy = null;
                webclient.Encoding = Encoding.UTF8;

                webclient.Headers.Add("Accept: application/vnd.twitchtv.v5+json");
                webclient.Headers.Add("Client-ID: " + ClientID);

                webclient.Headers.Add("Authorization: OAuth " + Token);

                string s = webclient.DownloadString("https://api.twitch.tv/kraken?oauth_token=" + Token);
                var result = JsonConvert.DeserializeObject<ApiResult.TokenResult>(s);
                Name = result.token.UserName;
                ID = result.token.UserId;
                return result;
            }
        }
    }
}