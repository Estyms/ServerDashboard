
using System.Collections.Generic;

namespace ServerDashboard
{
    class SerializedPlayer
    {
        public string name = "";
        public int health = 100;
        public int mana = 20;
        public Dictionary<string, int> inventory = new Dictionary<string, int>();
    }
}
