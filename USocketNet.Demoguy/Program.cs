using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using USocketNet;
using USocketNet.Model;

namespace SocketIOClient.Sample
{

    class Program
    {
        static async Task Main(string[] args)
        {
            USNDelivery.Instance.Initialize(
                new USNOptions(false, "localhost", 10), 
                new USNCreds("94", "cK4FK5CNr93YqJ7QD2vZmC6PNCBdxGYK6ilRHNkbzuT")
            );
            USNDelivery.Instance.OnConnected = OnConnection;
            USNDelivery.Instance.OnOrderStatus = OnOrderStatus;
            USNDelivery.Instance.Connect();

            //Received Message with Callback.
            /*socket.On("pri", response =>
            {
                Console.WriteLine("Listener: " + response.ToString());
            });*/

            Console.ReadLine();
        }

        private static void OnConnection(string events)
        {
            /*throw new NotImplementedException();*/
            Console.WriteLine("Connected! " + events);

            USNDelivery.Instance.JoinOrderChannel("abc", null);

            OrderItem oitem = new OrderItem();
            oitem.key = "abc";
            USNDelivery.Instance.SetOrderStatus(oitem, null);
        }

        private static void OnOrderStatus(OrderItem order)
        {
            /*throw new NotImplementedException();*/
            Console.WriteLine("Order Status: " + order.status.ToString());
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
