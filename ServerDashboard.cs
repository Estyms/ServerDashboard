using Terraria.ModLoader;

namespace ServerDashboard
{
	public class ServerDashboard : Mod
	{
		Server		server;
		ServerInfos sInfos;

		public ServerDashboard()
        {
			sInfos = new ServerInfos();
			server = new Server(sInfos);
			server.Start();	
        }

        public override void Unload()
        {
			server.Stop();
        }


    }
}