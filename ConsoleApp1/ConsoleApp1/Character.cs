using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    public class Character
    {
        public int level;
        public string name;
        public string job;
        public float atk;
        public int def;
        public int hp;
        public int maxhp;
        public int gold;
        public List<item> inventory;
        public List<Potion> potionsInverntory;
        public int exp;
        public int mp;
        public int maxmp;
        public Character(string name)
        {
            this.name = name;
            level = 1;
            job = "전사";
            atk = 10;
            def = 5;
            hp = 100;
            maxhp = 100;
            gold = 1500;
            inventory = new List<item>();
            potionsInverntory = new List<Potion>();
            exp = 0;
            maxmp = 50;
            mp = 50;
        }
        public void ClearStage()
        {
            exp++;
            if (level == exp)
            {
                level++;
                exp = 0;
                atk += 0.5f;
                def += 1;
            }
        }

    }
}
