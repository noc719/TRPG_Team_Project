namespace ConsoleApp1
{
    public class Potion
    {
        public string PotionName { get; set; }
        public string PotionStat { get; set; }
        public int PotionStatNum { get; set; }
        public string PotionContent { get; set; }
        public int PotionPrice { get; set; }
        public bool PotionIsBought { get; set; }
        public bool PotionIsEquipped { get; set; }
        public int Quantity { get; set; }

        public Potion(string potionName, string potionStat, int potionStatNum, string potionContent, int potionPrice, int quantity = 1, bool potionIsBought = false, bool potionIsEquipped = false)
        {
            PotionName = potionName;
            PotionStat = potionStat;
            PotionStatNum = potionStatNum;
            PotionContent = potionContent;
            PotionPrice = potionPrice;
            PotionIsBought = potionIsBought;
            PotionIsEquipped = potionIsEquipped;
            Quantity = quantity;
        }

        public string PotionItemInfo(string position)
        {
            string equippedIndicator = (PotionIsEquipped && position == "inventory") ? "" : "   ";
            return $"{PotionName}(x{Quantity}){equippedIndicator}| {PotionStat} + {PotionStatNum} | {PotionContent}";
        }
    }
}