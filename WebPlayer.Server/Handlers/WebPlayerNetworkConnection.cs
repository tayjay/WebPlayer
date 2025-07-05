using System;
using Mirror;
using NetworkManagerUtils.Dummies;
using WebPlayer.Server.Web;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Handlers
{
    public class WebPlayerNetworkConnection : NetworkConnectionToClient
    {
        
        private const string FakeAddress = "127.0.0.1";
        private static int _idGenerator = (int) ushort.MaxValue-10000;
        
        private Client client;

        public WebPlayerNetworkConnection(Client client)
            : base(_idGenerator--)
        {
            this.client = client;
        }

        public override string address { get; } = FakeAddress;

        public override void Send(ArraySegment<byte> segment, int channelId = 0)
        {
            WsServer.Server.SendMessage(client, segment.ToString());
        }

        protected override void SendToTransport(ArraySegment<byte> segment, int channelId = 0)
        {
            WsServer.Server.SendMessage(client, segment.ToString());
        }
        
    }
}