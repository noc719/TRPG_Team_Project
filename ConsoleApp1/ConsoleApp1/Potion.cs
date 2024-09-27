namespace ConsoleApp1
{
    [Serializable]
    public class Potion
    {
        public string potionName { get; }
        public int healAmount { get; }
        public string potionDescribe { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

        public Potion(string name, int healAmount, string describe, int price, int quantity)
        {
            potionName = name;
            healAmount = healAmount;
            potionDescribe = describe;
            price = price;
            quantity = quantity;
        }
    }
}