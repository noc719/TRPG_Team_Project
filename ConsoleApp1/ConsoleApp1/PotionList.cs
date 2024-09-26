using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class PotionList
    {
        private List<Potion> potions = new List<Potion>();
        public PotionList()
        {
            potions.Add(new Potion("HP 포션", 30, "HP를 30 회복합니다.", 50, 10));
            potions.Add(new Potion("중형 HP 포션", 50, "HP를 50 회복합니다.", 70, 10));
            potions.Add(new Potion("MP 포션", 30, "MP를 30 회복합니다.", 50, 10));
            potions.Add(new Potion("중형 MP 포션", 50, "MP를 50 회복합니다.", 70, 10));
        }

        public void Display()
        {
            foreach (var potion in potions)
            {
                Console.WriteLine($"- {potion.potionName} | 회복량: +{potion.healAmount} | {potion.potionDescribe} | {potion.price} G | X{potion.quantity}");
            }
        }

        //method move into shop? well, if everyone agree with this. yeah
        //public void DisplayPotions()
        //{
            //potionList.Display();
        //}

        //potionsInverntory
        public void AddPotion(Potion newPotion)
        {
            var existingPotion = potions.Find(p => p.potionName == newPotion.potionName);
            if (existingPotion != null)
            {
                existingPotion.quantity += newPotion.quantity;
            }
            else
            {
                //potions.Add(newPotion);
            }
        }

        public List<Potion> GetPotions()
        {
            return potions;
        }

        //method which is under this have to move into inventory.
        //private PotionList potionList;
        //potionList = new PotionList();
        //public void UsePotion(string potionName, Character me)
        //{
            //Potion potion = potionList.GetPotions().Find(p => p.PotionName == potionName);

            //if (potion != null && potion.Quantity > 0)
            //{
                //int potentialHealth = me.hp + potion.HealAmount;
                //if (potentialHealth > me.maxhp)
                //{
                    //me.hp = me.maxhp;
                    //Console.WriteLine("최대 체력에 도달했습니다.");
                //}
                //else
                //{
                    //me.hp = potentialHealth;
                    //Console.WriteLine("회복을 완료했습니다.");
                //}

                //potion.Quantity--;

                //if (potion.Quantity <= 0)
                //{
                    //potionList.GetPotions().Remove(potion);
                //}
            //}
            //else
            //{
                //Console.WriteLine("포션이 부족합니다.");
            //}
        //}
    }
}