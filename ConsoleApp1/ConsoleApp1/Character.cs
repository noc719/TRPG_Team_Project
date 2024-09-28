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
        public List<Quest> quest;
        public int exp;
        public int mp;
        public int maxmp;
        public int questMinionKill;//미니언 죽인 횟수
        public int questMaxionKill;//대포미니언 죽인 횟수
        public int questVoidBugKill;//공허충 죽인 횟수

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
            quest = new List<Quest>();
            exp = 0;
            maxmp = 50;
            mp = 50;
            questMinionKill = 0;//미니언 죽인 횟수
            questMaxionKill = 0;//대포미니언 죽인 횟수
            questVoidBugKill = 0;//공허충 죽인 횟수
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
