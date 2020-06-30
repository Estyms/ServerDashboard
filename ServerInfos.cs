using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ServerDashboard
{
    public class ServerInfos
    {

        private ServerDashboard instance;

        private DateTime bootTime;

        public ServerInfos()
        {
            bootTime = DateTime.Now;
        }

        public void getMods(StreamWriter sw)
        {
           sw.WriteLine(JsonConvert.SerializeObject(ModLoader.Mods.Select(var => var.Name).ToArray()));
        }
        
        public void getPlayers(StreamWriter sw)
        {
            List < SerializedPlayer > playerList = new List<SerializedPlayer>();

            // Get the list of Players on the server
            Player[] players = Main.player.Where(player => player.name != "").ToArray();
            // Loop over every players
            foreach (Player player in players)
            {
                SerializedPlayer serializedPlayer = new SerializedPlayer();
                serializedPlayer.name       = player.name;
                serializedPlayer.health     = player.statLife;
                serializedPlayer.mana       = player.statMana;
                foreach (Item item in player.inventory)
                {
                    if (item.Name != "") serializedPlayer.inventory.Add(item.Name, item.stack);
                }
                playerList.Add(serializedPlayer);
            }

            // Convert the list to an array then return it
            sw.WriteLine(JsonConvert.SerializeObject(playerList));
        }

        public void getUptime(StreamWriter sw)
        {
            SerializedUptime uptime = new SerializedUptime();
            uptime.bootDate = bootTime.ToString("dd-MM-yyyy HH:mm:ss");
            
            TimeSpan interval = DateTime.Now.Subtract(bootTime);
            uptime.elapsedTime = interval.ToString(@"hh\:mm\:ss");
            
            sw.WriteLine(JsonConvert.SerializeObject(uptime));
        }
    }
}