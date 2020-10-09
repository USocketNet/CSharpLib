using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using USocketNet;
using USocketNet.Model;

namespace SocketIOClient.Sample
{

    class Program
    {
        static async Task Main(string[] args)
        {
            USNMessage.Instance.Initialize(
                new USNOptions(false, "localhost", 10), 
                new USNCreds("1", "0zS59CEKoHTgpjBwzFBpVpFR83ogYaKeJIoeVQkzvtB")
            );
            USNMessage.Instance.Connect();

            //Received Message with Callback.
            /*socket.On("pri", response =>
            {
                Console.WriteLine("Listener: " + response.ToString());
            });*/

            Console.ReadLine();
        }
    }

    class ByteResponse
    {
        public string ClientSource { get; set; }

        public string Source { get; set; }

        [JsonProperty("bytes")]
        public byte[] Buffer { get; set; }
    }

    class ClientCallbackResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("bytes")]
        public byte[] Bytes { get; set; }
    }
}
