using System;
using System.ComponentModel;

namespace WebPlayer.Server
{
    public class Config
    {
        [Description("What port the WebPlayer server should run on. Default is 5000.")]
        public int Port { get; set; } = 5000;
        
        [Description("Size of the WebSocket buffer. Default is 1024.")]
        public int WsBufferSize { get; set; } = 1024;
        
        // API Key
        [Description(
            "API Key for the WebPlayer server. This is used to authenticate requests to the server. Default is a random UUID.")]
        public string ApiKey { get; set; } = Guid.NewGuid().ToString();
    }
}