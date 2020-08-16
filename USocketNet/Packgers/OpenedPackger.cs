using Newtonsoft.Json;
using USocketNet.Response;

namespace USocketNet.Packgers
{
    public class OpenedPackger : IUnpackable
    {
        public void Unpack(SocketIO client, string text)
        {
            if (text.StartsWith("{\"sid\":\""))
            {
                client.Open(JsonConvert.DeserializeObject<OpenResponse>(text));
            }
        }
    }
}
