using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ID;
using System.Text.RegularExpressions;

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
            SerializedMod serializedMod = new SerializedMod();
            foreach (Mod mod in ModLoader.Mods)
            {
                serializedMod.modInfos.Add(mod.DisplayName, mod.Name);
            }
           sw.WriteLine(JsonConvert.SerializeObject(serializedMod));
           
        }
        
        public void getPlayers(StreamWriter sw)
        {
            // Initialize list of Serialized Player
            List < SerializedPlayer > playerList = new List<SerializedPlayer>();

            // Loop over every players
            foreach (Player player in Main.player.Where(player => player.active).ToArray())
            {
                // Create a serializedPlayer object
                SerializedPlayer serializedPlayer = new SerializedPlayer();
                serializedPlayer.name       = player.name;
                serializedPlayer.health     = player.statLife;
                serializedPlayer.mana       = player.statMana;

                // Create Inventory Map
                foreach (Item item in player.inventory.Where((item, index) => item.Name != "" && index < 50).ToArray())
                {
                    if (item.prefix != 0)
                    {
                        string prefix = PrefixID.GetUniqueKey(item.prefix);
                        prefix = prefix.Replace("Terraria ", "") + " ";
                        serializedPlayer.inventory.Add(prefix + item.Name, item.stack);
                    }
                    else
                    {
                        serializedPlayer.inventory.Add(item.Name, item.stack);
                    }
                }

                // Create Ammunitions List
                foreach (Item item in player.inventory.Where((item, index) => item.Name != "" && index > 53).ToArray())
                {
                    serializedPlayer.ammunitions.Add(item.Name, item.stack);
                }

                // Create Coins List
                foreach (Item item in player.inventory.Where((item, index) => item.Name != "" && index > 49 && index < 54).ToArray())
                {
                    serializedPlayer.coins.Add(item.Name, item.stack);
                }

                // Create Piggy Bank Map
                foreach (Item item in player.bank.item.Where(item => item.Name != "").ToArray())
                {
                    if (item.prefix != 0)
                    {
                        string prefix = PrefixID.GetUniqueKey(item.prefix);
                        prefix = prefix.Replace("Terraria ", "") + " ";
                        serializedPlayer.piggyBank.Add(prefix + item.Name, item.stack);
                    }
                    else
                    {
                        serializedPlayer.piggyBank.Add(item.Name, item.stack);
                    }
                }

                // Create Safe Map
                foreach (Item item in player.bank2.item.Where(item => item.Name != "").ToArray())
                {
                    if (item.prefix != 0)
                    {
                        string prefix = PrefixID.GetUniqueKey(item.prefix);
                        prefix = prefix.Replace("Terraria ", "") + " ";
                        serializedPlayer.safe.Add(prefix + item.Name, item.stack);
                    }
                    else
                    {
                        serializedPlayer.safe.Add(item.Name, item.stack);
                    }
                }

                // Create Defender Forge Map
                foreach (Item item in player.bank3.item.Where(item => item.Name != "").ToArray())
                {
                    if (item.prefix != 0)
                    {
                        string prefix = PrefixID.GetUniqueKey(item.prefix);
                        prefix = prefix.Replace("Terraria ", "") + " ";
                        serializedPlayer.defenderForge.Add(prefix + item.Name, item.stack);
                    }
                    else
                    {
                        serializedPlayer.defenderForge.Add(item.Name, item.stack);
                    }
                }

                // Get List of Armor Items
                foreach (Item item in player.armor.Where((item,index) => item.Name != "" && index < 3).ToArray())
                {
                    Dictionary<string, string> itemInfos = new Dictionary<string, string>();

                    if (item.prefix != 0)
                    {
                        string prefix = PrefixID.GetUniqueKey(item.prefix);
                        prefix = prefix.Replace("Terraria ", "") + " ";
                        itemInfos.Add("Prefix", prefix);
                    }

                    int index = Array.FindIndex(player.armor, (c => c.Name == item.Name));
                    if (player.dye[index].Name != "") itemInfos.Add("Dye", player.dye[index].Name);

                    serializedPlayer.armor.Add(item.Name, itemInfos);
                }

                // Get List of Accessories
                foreach (Item item in player.armor.Where((item, index) => item.Name != "" && index > 2).ToArray())
                {
                    Dictionary<string, string> itemInfos = new Dictionary<string, string>();
                    
                    if (item.prefix != 0)
                    {
                        string prefix = PrefixID.GetUniqueKey(item.prefix);
                        prefix = prefix.Replace("Terraria ", "") + " ";
                        itemInfos.Add("Prefix", prefix);
                    }

                    int index = Array.FindIndex(player.armor, (c => c.Name == item.Name));
                    if (player.dye[index].Name != "") itemInfos.Add("Dye", player.dye[index].Name);

                    serializedPlayer.accessories.Add(item.Name, itemInfos);
                }

                // Create Equipments List
                foreach (Item item in player.miscEquips.Where(item => item.Name != "").ToArray())
                {
                    Dictionary<string, string> itemInfos = new Dictionary<string, string>();
                    int index = Array.FindIndex(player.miscEquips, (c => c.Name == item.Name));
                    if (player.miscDyes[index].Name != "") itemInfos.Add("Dye", player.miscDyes[index].Name);
                    serializedPlayer.equipments.Add(item.Name, itemInfos);
                }

                // Create Buff Map
                foreach (int buff in player.buffType.Where(buff => buff != 0))
                {
                    // Getting the buff Name with some Regex
                    string buffName = BuffID.GetUniqueKey(buff);
                    buffName = buffName.Replace("Terraria ", "");
                    buffName = Regex.Replace(buffName, "(\\B[A-Z])", " $1");
                    serializedPlayer.buffs.Add(buffName);
                }

                // Add Serialized Player to List
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

        public void getWorld(StreamWriter sw)
        {
            try {
                SerializedWorld world = new SerializedWorld();
                WorldFileData currentWorld = Main.ActiveWorldFileData;
                world.name = currentWorld.Name;
                world.creationTime = currentWorld.CreationTime;
                world.hardmode = currentWorld.IsHardMode;
                world.evilBiome = currentWorld.HasCorruption ? "corruption" : "crimson";
                world.size = currentWorld.WorldSizeName;
                world.seed = currentWorld.Seed;
                world.expert = currentWorld.IsExpertMode;
                sw.WriteLine(JsonConvert.SerializeObject(world));
            } catch (NullReferenceException _){ 
                SerializedError serializedError = new SerializedError(500, "World isn't started yet on the server.");
                sw.WriteLine(JsonConvert.SerializeObject(serializedError));
            } catch (Exception e)
            {
                SerializedError serializedError = new SerializedError(500, e.Message);
                sw.WriteLine(JsonConvert.SerializeObject(serializedError));
            }
        }
    }
}