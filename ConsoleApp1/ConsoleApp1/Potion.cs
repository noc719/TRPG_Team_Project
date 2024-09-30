namespace ConsoleApp1
{
    [Serializable]
    public class Potion
    {
        public string potionType { get; } //hp, mp
        public string potionName { get; }
        public int potionHealAmount { get; }
        public string potionDescribe { get; set; }
        public int potionQuantity { get; set; }

        public Potion(string type, string name, int healAmount, string describe, int quantity)
        {
            potionType = type;
            potionName = name;
            potionHealAmount = healAmount;
            potionDescribe = describe;
            potionQuantity = quantity;
        }
    }
}