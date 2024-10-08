﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Inventory
    {
        private static List<Quest> battlequestlist = Program.questlist.quests;
        public static void EnterInventory(Character me) //인벤토리
        {
            int a = 0;
            int count = 0;
            Console.Clear();
            Console.WriteLine($"인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n");
            foreach (item item in me.inventory)//아이템 목록 출력
            {
                Console.Write($"- ");
                if (me.inventory[count].isEquipped == true)//장착되어 있을 경우 앞에 "[E]" 추가
                {
                    Console.Write("[E]");
                }
                Console.WriteLine(item.itemInfo("inventory"));
                count++;
            }
            Console.WriteLine();

            Console.WriteLine("[포션 목록]\n");
            foreach (Potion potion in me.potionsInverntory)
            {
                Console.WriteLine($"- {potion.potionName} X {potion.potionQuantity}");
            }
            Console.WriteLine();

            Console.Write("\n1. 장착 관리\n2. 회복포션 사용하기\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
            while (a == 0)
            {
                string? choice = Console.ReadLine();
                if (choice == "0")
                {
                    Program.GameStart(me);
                    a = 1;
                }
                else if (choice == "1")
                {
                    Equip(me);//장착관리
                    a = 1;
                }
                else if (choice == "2")
                {
                    UsePotion(me); //potion
                    a = 1;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
        public static void Equip(Character me)//아이템 장착
        {
            int b, a = 0;

            while (a == 0)
            {
                b = 0;
                int count = 0;
                Console.Clear();
                Console.WriteLine($"인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n");
                foreach (item item in me.inventory)
                {
                    count += 1;
                    Console.Write($"- {count} ");
                    if (me.inventory[count - 1].isEquipped == true)
                    {
                        Console.Write("[E]");
                    }
                    Console.WriteLine(me.inventory[count - 1].itemInfo("inventory"));
                }
                Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                while (b == 0)
                {
                    string? choice = Console.ReadLine();
                    if (choice == "0")
                    {
                        EnterInventory(me);
                        a = 1;
                        b = 1;
                    }
                    else if (int.Parse(choice) <= count)
                    {
                        //item selected = itemlist.items[me.inventory[int.Parse(choice)-1]];
                        item selected = me.inventory[int.Parse(choice) - 1];
                        if (selected.isEquipped == true)
                        {
                            //itemlist.items[me.inventory[int.Parse(choice) - 1]].isEquipped = false;
                            selected.isEquipped = false;
                            if (selected.stat == "방어력")
                            {
                                me.def -= selected.statNum;
                            }
                            else if (selected.stat == "공격력")
                            {
                                me.atk -= selected.statNum;
                            }
                        }
                        else if (selected.isEquipped == false)
                        {
                            ////////////////////////////////////////////////////////////
                            /// 아이템 장착시 퀫스트 진행도 증가
                            /////////////////////////////////////////////////////////////
                            foreach (var quest in battlequestlist)
                            {
                                if (me.questItemEquip < 1 && quest.questTitle == "장비 장착하기" && quest.isQuestAccepted)  // 퀘스트가 아이템 장착 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                                {
                                    me.questItemEquip++;
                                    quest.progressCount = me.questItemEquip;
                                    break; // 퀘스트 처리 후 반복문 종료
                                }
                            }
                            //itemlist.items[me.inventory[int.Parse(choice) - 1]].isEquipped = true;
                            if (selected.stat == "방어력")
                            {
                                foreach (item item in me.inventory)
                                {
                                    if (item.isEquipped == true && item.stat == "방어력")
                                    {
                                        item.isEquipped = false;
                                        me.def -= item.statNum;
                                    }
                                }
                                me.def += selected.statNum;
                            }
                            else if (selected.stat == "공격력")
                            {
                                foreach (item item in me.inventory)
                                {
                                    if (item.isEquipped == true && item.stat == "공격력")
                                    {
                                        item.isEquipped = false;
                                        me.atk -= item.statNum;
                                    }
                                }
                                me.atk += selected.statNum;
                            }
                            selected.isEquipped = true;
                        }
                        b = 1;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    }
                }
            }
        }

        public static void UsePotion(Character me)
        {
            Console.Clear();
            int count = 1; //if player have a potion, player needs a number to choose one.
            Console.WriteLine($"인벤토리 - 포션 사용\n보유 중인 포션을 사용할 수 있습니다.\n");
            if (me.potionsInverntory.Count == 0)
            {
                Console.WriteLine("소지한 포션이 없습니다.");
                Console.WriteLine();

                Console.Write("0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
                while (true)
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 0)
                    {
                        EnterInventory(me);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    }
                }
            }
            else
            {
                Console.WriteLine("[포션 목록]\n");
                foreach (Potion potion in me.potionsInverntory) //potion list
                {
                    Console.WriteLine($"{count}. {potion.potionName} | 회복량: + {potion.potionHealAmount} | {potion.potionDescribe} | X {potion.potionQuantity}");
                    count++;
                }
                Console.WriteLine();

                Console.Write("사용하실 포션 번호를 입력해 주세요.\n>>");

               
                while (true)
                { 
                    int n = int.Parse(Console.ReadLine());
                    if (0 < n && n <= me.potionsInverntory.Count)
                    {
                        Potion potion = me.potionsInverntory[n - 1];

                        if (0 < potion.potionQuantity)
                        {
                            if (potion.potionType == "HP") //hp
                            {
                                if (me.hp < me.maxhp)
                                {
                                    potion.potionQuantity--;
                                    if (potion.potionQuantity == 0) //if potion quantity is 0, remove it from inventory
                                    {
                                        me.potionsInverntory.RemoveAt(n - 1);
                                    }

                                    me.hp += potion.potionHealAmount;

                                    if (me.hp > me.maxhp) //hp can't increase than maxhp.
                                    {
                                        me.hp = me.maxhp;
                                        Console.WriteLine("체력이 완전히 회복되었습니다.");
                                        Console.WriteLine();

                                        Exit(me);
                                        break;
                                    }

                                    Console.WriteLine($"{potion.potionName}을 사용하여 + {potion.potionHealAmount}을 회복했습니다.\n현재 HP: {me.hp}/{me.maxhp}");
                                    Console.WriteLine();

                                    Exit(me);
                                    break;
                                }
                                else //if(me.hp > me.maxhp)
                                {
                                    Console.WriteLine("체력이 최대치에 도달하여 포션을 사용할 수 없습니다.");
                                    Exit(me);
                                    break;
                                }
                            }
                            else if (potion.potionType == "MP") //mp
                            {
                                if (me.mp < me.maxmp)
                                {
                                    potion.potionQuantity--;
                                    if (potion.potionQuantity == 0) //if potion quantity is 0, remove it from inventory
                                    {
                                        me.potionsInverntory.RemoveAt(n - 1);
                                    }

                                    me.mp += potion.potionHealAmount;

                                    if (me.mp > me.maxmp) //mp can't increase than maxmp.
                                    {
                                        me.mp = me.maxmp;
                                        Console.WriteLine("마나가 완전히 회복되었습니다.");
                                        Console.WriteLine();

                                        Exit(me);
                                        break;
                                    }

                                    Console.WriteLine($"{potion.potionName}을 사용하여 +{potion.potionHealAmount}을 회복했습니다.\n현재 MP: {me.mp}/{me.maxmp}");
                                    Console.WriteLine();

                                    Exit(me);
                                    break;
                                }
                                else //if(me.mp > me.maxmp)
                                {
                                    Console.WriteLine("마나가 최대치에 도달하여 포션을 사용할 수 없습니다.");

                                    Exit(me);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");

                    }
                }
            }
        }

        public static void Exit(Character me)
        {
            Console.Write("0. 나가기\n>>");
            while (true)
            {
                string? choice = Console.ReadLine();
                if (choice == "0")
                {
                    EnterInventory(me);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
    }
}
