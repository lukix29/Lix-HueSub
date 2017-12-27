using LX29_Hue_Sub.Properties;

namespace Hue.HTTP
{
    public class UrlProvider
    {
        private string ip;

        public UrlProvider(string ip)
        {
            this.ip = ip;

            if (!this.ip.StartsWith("http://"))
                this.ip = "http://" + this.ip;

            this.ip = this.ip.TrimEnd('/');
            this.ip = this.ip.Replace("/description.xml", "");
        }

        internal string GetLampUrl(int lightKey)
        {
            return ip + "/api/" + Settings.Default.BridgeApiKey + "/lights/" + lightKey + "/state";
        }

        internal string GetRegisterUrl()
        {
            return ip + "/api";
        }

        internal string GetStatusUrl()
        {
            return ip + "/api/" + Settings.Default.BridgeApiKey;
        }
    }
}