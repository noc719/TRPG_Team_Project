using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void Reward(Character me )
        {
            Random random = new Random();      
           

            int rewardGold = level * random.Next(50, 151);  //50~150 사이의 값에 몬스터의 레벨만큼 
            int potionDrop = random.Next(1, 101);  //포션휙득 확률 백분율
            
            int itemDrop = random.Next(1, 101); //장비 휙득 확률 백분율
            
            Console.WriteLine("[휙득 아이템]");
            Console.WriteLine("{0}", rewardGold);

            me.gold += rewardGold;
            if (potionDrop <= 20)
            {

                

                int potionType = random.Next(1, 4); // 포션 타입
                
                Console.WriteLine("포션 - {}"); // 랜덤한 갯수의 포션


                List<Potion> potions = new List<Potion>();
               

                potions = Program.potionlist.GetPotions();
                PotionInventory.AddPotionToInventory(potions[potionType], me);
                    //potions[potionType]




            }
            //if(itemDrop == 1) 
            //{
            //    int itemType
            //    Console.WriteLine("장비 이름 - 1"); 
                

                
            //    //휙득하지 못한 장비 
            //}
            
            


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












