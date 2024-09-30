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

        public void Reward(Character me )
        {
            Random random = new Random();      
           

            int rewardGold = level * random.Next(50, 151);  //50~150 사이의 값에 몬스터의 레벨만큼 
            int potionDrop = random.Next(1, 101);  //포션획득득 확률 백분율
            int itemDrop = random.Next(1, 101); //장비 획득 확률 백분율
            int rewardExp = level * 1; //경험치 부분


            Battle.saveGold += rewardGold;
            Battle.saveExp += rewardExp;


            if (potionDrop <= 20) //포션 부분
            {
                int potionType = random.Next(1, 4); // 포션 타입

               

                Battle.pickupPotion = Program.potionlist.GetPotions();
               

                
                // 랜덤한 갯수의 포션


            }
            if(itemDrop == 1)
            {
                //아이템 타입에 따른 
                int itemType = random.Next(0,Program.itemlist.items.Length); 

                item randomItem = Program.itemlist.items[itemType]; 
                if (randomItem.isBought == true) //중복된 장비일 경우
                {
                    
                    Console.WriteLine("중복 장비 휙득 - {0}G",randomItem.price/3);  //장비 원값의 3분의 1 가격을 얻음
                    Battle.saveGold += randomItem.price/3;

                }
                else
                {
                    Battle.pickupItem.Add(randomItem);
                    Console.WriteLine("{0} - 1",randomItem.name);

                    
                    

                }
                
         
            }
            
            


        }

        public void ClearReward(Character me)
        {
            Console.Write("exp {0} -> ", me.exp); //경험치 부분
            me.exp += Battle.saveExp;
            Console.WriteLine(" {0}", me.exp);

            Console.WriteLine();

            Console.WriteLine("[휙득 아이템]");
            foreach (item item in Battle.pickupItem)
            {
                me.inventory.Add(item);
                Console.WriteLine(item.name + " - 1");
            }

            Console.WriteLine("{0}", Battle.saveGold);   //획득 골드 부분
            me.gold += Battle.saveGold;

           foreach (Potion dropedPotion in Battle.pickupPotion)
            {
                
                PotionInventory.AddPotionToInventory(dropedPotion, me);
            }

            Console.WriteLine("획득한 포션 - {0} 개", Battle.pickupPotion.Count);



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












