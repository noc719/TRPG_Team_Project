using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Monster
    {

        public int level { get; set; }
        public string name { get; set; }
        public int hp { get; set; }
        public int def { get; set; }
        public int atk { get; set; }



        //Reward 메소드의 정보를 받아옴





        //몬스터 정보
        //몬스터 스폰을 랜덤하게
        public Monster(int level, string name, int hp, int def, int atk)
        {

            this.level = level;
            this.name = name;
            this.hp = hp;
            this.def = def;
            this.atk = atk;
        }

        public void Reward(Character me)
        {
            Random random = new Random();


            int rewardGold = level * random.Next(20, 51);  //20~50 사이의 값에 몬스터의 레벨만큼 
            int potionDrop = random.Next(1, 101);  //포션획득득 확률 백분율
            int itemDrop = random.Next(1, 101); //장비 획득 확률 백분율
            int rewardExp = level * 1; //경험치 부분


            Battle.saveGold += rewardGold;
            Battle.saveExp += rewardExp;


            if (potionDrop <= 20) //포션 부분
            {
                int potionType = random.Next(0, 4); // 포션 타입

                

                Battle.pickupPotion.Add(Program.potionlist.potions[potionType]);



                // 랜덤한 갯수의 포션


            }
            if (itemDrop <=5)
            {
                //아이템 타입에 따른 
                int itemType = random.Next(0, Program.itemlist.items.Length);

                item randomItem = Program.itemlist.items[itemType];Battle.pickupItem.Add(randomItem);
            }




        }

    }    

    public class Minion : Monster
    {

        public Minion() : base(2, "미니언", 15, 10, 5)
        {

        }

    }

    public class VoidInsec : Monster
    {

        public VoidInsec() : base(3, "공허충", 10, 10, 9)
        {

        }

    }
    public class CannonMinion : Monster
    {

        public CannonMinion() : base(5, "대포미니언", 25, 10, 8)
        {

        }

    }



}












