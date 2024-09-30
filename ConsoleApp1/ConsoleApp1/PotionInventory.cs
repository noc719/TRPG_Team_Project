namespace ConsoleApp1
{
    internal class PotionInventory
    {
        private List<Potion> potions;

        //shop & dungeon
        public static void AddPotionToInventory(Potion potion, Character me)
        {
            var existingPotion = me.potionsInverntory.Find(p => p.potionName == potion.potionName);
            if (existingPotion == null)
            {
                me.potionsInverntory.Add(potion);
            }
            else
            {
               existingPotion.potionQuantity += potion.potionQuantity;
            }
        }
    }
}