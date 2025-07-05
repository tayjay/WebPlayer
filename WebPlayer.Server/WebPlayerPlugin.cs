using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using WebPlayer.Server.Handlers;
using WebPlayer.Server.Web;

namespace WebPlayer.Server
{
    public class WebPlayerPlugin : Plugin<Config>
    {
        public static WebPlayerPlugin Instance { get; private set; }
        public FakePlayerHandler FakePlayerHandler { get; private set; }
        
        public override void Enable()
        {
            Instance = this;
            WsServer.Start();
            FakePlayerHandler = new FakePlayerHandler();
        }

        public override void Disable()
        {
            WsServer.Stop();
            FakePlayerHandler = null;
        }

        public override string Name { get; } = "Web Player";
        public override string Description { get; } = "A plugin that allows players to join the game from an external web client.";
        public override string Author { get; } = "TayTay";
        public override Version Version { get; } = new Version(0, 1, 0);
        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
    }
}