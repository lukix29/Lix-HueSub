using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace LixHueSub.Tipeee
{
    public class TipeeEventHandler
    {
        private Socket socket;
        public void Dispose()
        {
            socket?.Disconnect();
        }
        public void NewEventReceived(object o)
        {

        }
        public async void LoginTipeee(string access_token, Action action)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Proxy = null;

                    var json = await wc.DownloadStringTaskAsync("https://api.tipeeestream.com/v1.0/events.json?apiKey=" + access_token + "&type[]=follow&limit=1");
                    JObject jobj = JObject.Parse(json);
                    var username = jobj["datas"]["items"][0]["user"].Value<string>("username");

                    var room = access_token;
                    var obj = new { room = access_token, username = username };
                    socket = IO.Socket("https://sso.tipeeestream.com:4242");
                    socket.On("connect", () =>
                    {
                        socket.Emit("join-room", obj);

                        socket.On("join-room", () =>
                             {
                                 socket.On("new-event", (data) => NewEventReceived(data));
                                 action?.Invoke();
                             });
                    });
                    socket.Connect();

                    //var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //socket.Connect("sso.tipeeestream.com", 4242);
                    //var username = jobj.Value<string>("username");
                }
            }
            catch
            {

            }
        }
    }
}
