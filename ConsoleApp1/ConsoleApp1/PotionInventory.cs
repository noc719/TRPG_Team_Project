namespace ConsoleApp1
{
    internal class PotionInventory
    {
        private List<Potion> potions;
        public void EnterPotionInventory(Character me)
        {            
            Console.WriteLine("[포션 목록]");

            foreach (Potion potion in me.potionsInverntory)
            {
                Console.WriteLine($"- {potion.potionName} X{potion.quantity}");
            }
            Console.WriteLine();

            Console.WriteLine("2. 회복포션 사용하기\n3. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
            
            int n;
            while (true)
            {
                n = int.Parse(Console.ReadLine());
                if (n==2)
                {
                    UsePotion(me);
                }
                else if(n==3)
                {
                    Program.GameStart(me);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n>>>");
                }
            }
        }

        //shop & dungeon
        public void AddPotionToInventory(Potion potion, Character me)
        {
            var existingPotion = potions.Find(p => p.potionName == potion.potionName);
            if (existingPotion == null)
            {
                me.potionsInverntory.Add(potion);
            }
            else
            {
               existingPotion.quantity += potion.quantity;
            }
        }
    
        public void UsePotion(Character me)
        {
            int count = 1;

            Console.WriteLine("[포션 목록]");
            foreach (Potion potion in me.potionsInverntory)
            {
                Console.WriteLine($"{count} {potion.potionName} | {potion.potionDescribe} | X{potion.quantity}");
                count++;
            }
            Console.WriteLine();

            Console.WriteLine("사용하실 포션을 입력해 주세요.\n>>>");
            int n = int.Parse(Console.ReadLine());

            if( 0 < n && n <= me.potionsInverntory.Count)
            {
                Potion potion = me.potionsInverntory[n-1];

                if (0 < potion.quantity)
                {
                    if (me.hp < me.maxhp)
                    {
                        potion.quantity--;
                        me.hp += potion.healAmount;

                        if(me.hp > me.maxhp) //hp can't increase than maxhp.
                        {
                            me.hp = me.maxhp;
                        }

                        Console.WriteLine($"{potion.potionName}을 사용하여 +{potion.healAmount}을 회복했습니다.\n현재 HP: {me.hp}/{me.maxhp}");
                        EnterPotionInventory(me);
                    }
                    else //if(me.hp > me.maxhp)
                    {
                        Console.WriteLine("체력이 최대치에 도달하여 포션을 더 이상 사용할 수 없습니다.");
                    }
                }
            }
        }
    }
}