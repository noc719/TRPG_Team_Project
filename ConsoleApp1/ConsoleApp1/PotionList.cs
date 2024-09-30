using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    [Serializable]
    public class PotionList
    {
        public List<Potion> potions = new List<Potion>();

        public PotionList()
        {
            potions.Add(new Potion("HP 포션", 30, "HP를 30 회복합니다.", 50, 5));
            potions.Add(new Potion("중형 HP 포션", 50, "HP를 50 회복합니다.", 70, 5));
            potions.Add(new Potion("MP 포션", 30, "MP를 30 회복합니다.", 50, 5));
            potions.Add(new Potion("중형 MP 포션", 50, "MP를 50 회복합니다.", 70, 5));
        }

        public void AddPotion(Potion newPotion)
        {
            var existingPotion = potions.Find(p => p.potionName == newPotion.potionName);
            if (existingPotion != null)
            {
                existingPotion.quantity += newPotion.quantity;
            }
            else
            {
                potions.Add(newPotion);
            }
        }

        public List<Potion> GetPotions()
        {
            return potions;
        }
    }
}