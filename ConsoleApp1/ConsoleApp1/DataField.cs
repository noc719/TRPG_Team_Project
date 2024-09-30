using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Program;

namespace ConsoleApp1
{
    [Serializable]
    public class DataField
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
        public int exp;
        public ItemList itemList;
        public List<Potion> potionsInverntory;
        public PotionList potionlist;
        public int mp;
        public int maxmp;
        public int questMinionKill;//미니언 죽인 횟수
        public int questMaxionKill;//대포미니언 죽인 횟수
        public int questVoidBugKill;//공허충 죽인 횟수
        public int questItemBuy;//상점에서 아이템 구매
        public int questItemEquip;//인벤토리에서 장착아이템 장착
        public List<Quest> quests;
        public QuestList questlist;
    }
}
