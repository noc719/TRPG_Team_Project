using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class PotionList
    {
        public List<Potion> Potions { get; private set; } = new List<Potion>();

        public PotionList()
        {
            Potions.Add(new Potion("- HP 포션 | 회복량 +", "HP", 30, " | HP를 30회복합니다.", 50));
            Potions.Add(new Potion("- 중형 HP 포션 | 회복량 +", "HP",50, " | HP를 50회복합니다. ", 70));
            Potions.Add(new Potion("- MP 포션 | 회복량 +", "MP", 30, " | MP를 30회복합니다.", 50));
            Potions.Add(new Potion("- 중형 MP 포션 | 회복량 +", "MP", 50, " | MP를 50회복합니다.", 70));
        }

        public void AddPotion(Potion newPotion)
        {
            var existingPotion = Potions.Find(p => p.PotionName == newPotion.PotionName);
            if (existingPotion != null)
            {
                existingPotion.Quantity += newPotion.Quantity;
            }
            else
            {
                Potions.Add(newPotion);
            }
        }
        public void DisplayPotions()
        {
            foreach (var potion in Potions)
            {
                Console.WriteLine(potion.PotionItemInfo("inventory"));
            }
        }

        public void UsePotion(string potionName, Character me)
        {
            Potion potion = Potions.Find(p => p.PotionName.Contains(potionName) && p.PotionIsBought);
            
            if (potion != null)
            {
                int potentialHealth = me.hp + potion.PotionStatNum;
                if (potentialHealth > me.maxhp)
                {
                    me.hp = me.maxhp;
                    Console.WriteLine("최대 체력에 도달했습니다.");
                }
                else
                {
                    me.hp = potentialHealth;
                    Console.WriteLine("회복을 완료했습니다.");
                }
                potion.PotionIsBought = false;
            }
            else
            {
                Console.WriteLine("포션이 부족합니다.");
            }
        }
    }
}