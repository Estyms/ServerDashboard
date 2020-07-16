
using System;
using System.Collections.Generic;

namespace ServerDashboard
{
    class SerializedPlayer
    {
        public string name = "";
        public int health = 100;
        public int mana = 20;
        public Dictionary<string, int> inventory        = new Dictionary<string, int>();
        public Dictionary<string, int> ammunitions      = new Dictionary<string, int>();
        public Dictionary<string, int> coins            = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, string>> armor         = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> accessories   = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> equipments    = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, int> piggyBank        = new Dictionary<string, int>();
        public Dictionary<string, int> safe             = new Dictionary<string, int>();
        public Dictionary<string, int> defenderForge    = new Dictionary<string, int>();
        public List<string> buffs                       = new List<string>();
    }
}
