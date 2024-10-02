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
        public int expLimit = 10;
        public int mp;
        public int maxmp;
        public int questMinionKill;//미니언 죽인 횟수
        public int questMaxionKill;//대포미니언 죽인 횟수
        public int questVoidBugKill;//공허충 죽인 횟수
        public int questItemBuy;//상점에서 아이템 구매
        public int questItemEquip;//인벤토리에서 장착아이템 장착
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
            questItemBuy = 0;//상점에서 아이템 구매
            questItemEquip = 0;//인벤토리에서 장착아이템 장착
        }
       
        public void LevelSystem()
        {
            while (true)
            {
                if (exp > expLimit)
                {
                    exp -= expLimit;
                    expLimit += 20 + (level * 5);
                    level++;

                    atk += 0.5f;
                    def += 1;


                }
                else
                {
                    break;
                }
            }
        }
    }
}
