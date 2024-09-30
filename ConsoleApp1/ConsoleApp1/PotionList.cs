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
            potions.Add(new Potion("HP", "HP 포션", 30, "HP를 30 회복합니다.", 1));
            potions.Add(new Potion("HP", "중형 HP 포션", 50, "HP를 50 회복합니다.", 1));
            potions.Add(new Potion("MP", "MP 포션", 30, "MP를 30 회복합니다.", 1));
            potions.Add(new Potion("MP", "중형 MP 포션", 50, "MP를 50 회복합니다.", 1));
        }

        public Potion GetPotionByType(string potionType)
        {
            return potions.FirstOrDefault(p => p.potionType == potionType);
        }

        public List<Potion> GetPotions()
        {
            return potions;
        }
    }
}