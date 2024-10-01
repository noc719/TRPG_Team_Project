namespace ConsoleApp1
{
    internal class PotionInventory
    {
        private List<Potion> potions;

        //shop & dungeon
        public static void AddPotionToInventory(Potion potion, Character me)
        {
            var existingPotion = me.potionsInverntory.FirstOrDefault(p => p.potionName == potion.potionName);

            if( existingPotion == null)
            {
                me.potionsInverntory.Add(new Potion(potion.potionType, potion.potionName, potion.potionHealAmount, potion.potionDescribe, potion.potionQuantity));
            }
            else
            {
                existingPotion.potionQuantity += potion.potionQuantity;
            }
        }
    }
}