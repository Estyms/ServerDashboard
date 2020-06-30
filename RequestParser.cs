using System;
using System.Collections.Generic;
using System.IO;

namespace ServerDashboard
{
    public class RequestParser
    {

        Dictionary<string, Action<StreamWriter>> map = new Dictionary<string, Action<StreamWriter>>();
        public RequestParser(ServerInfos sInfos)
        {
            map.Add("mods",    sInfos.getMods   );
            map.Add("players", sInfos.getPlayers);
            map.Add("uptime",  sInfos.getUptime );
        }


        public void Parse(StreamWriter sw, string path)
        {
            Action<StreamWriter> function;
            map.TryGetValue(path, out function);
            try
            {
                function(sw);
            } catch
            {
                sw.WriteLine("Invalid request.");
            }
 
        }

    }
}