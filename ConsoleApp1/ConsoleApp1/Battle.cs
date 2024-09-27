using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Battle
    {
        public static int starthp;
        public static void EnterBattle(Character me, List<Monster> monsters)
        {
            Console.Clear();
            string? choice = "";
            List<Monster> monsterlist = new List<Monster>();
            if (monsters == null)
            {
                starthp = me.hp;
                Random random = new Random();
                int monsterCount = 0;
                monsterCount = random.Next(1, 5);
                for (int i = 1; i <= monsterCount; i++)
                {
                    int monsterSpawn = random.Next(1, 11);
                    Monster temp;
                    if (monsterSpawn <= 5)
                    {
                        temp = new Minion();
                    }
                    else if (monsterSpawn <= 8)
                    {
                        temp = new VoidInsec();
                    }
                    else
                    {
                        temp = new CannonMinion();
                    }
                    monsterlist.Add(temp);
                }
            }
            else
            {
                monsterlist = monsters;
            }
            Console.Write("Battle!!\n\n");
            foreach (Monster monster in monsterlist)
            {
                if (monster.hp > 0)
                {
                    Console.WriteLine($"Lv.{monster.level} {monster.name}  HP{monster.hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv.{monster.level} {monster.name}  Dead");
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\n\n1. 공격\n\n원하시는 행동을 입력해주세요.\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    Attack(me, monsterlist);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public static void Attack(Character me, List<Monster> monsterList)
        {
            Console.Clear();
            Console.Write("Battle!!\n\n");
            string? choice = "";
            int count = 0;
            foreach (Monster monster in monsterList)
            {
                count++;
                if (monster.hp > 0)
                {
                    Console.WriteLine($"{count} Lv.{monster.level} {monster.name}  HP{monster.hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{count} Lv.{monster.level} {monster.name}  Dead");
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\n\n0. 취소\n\n대상을 선택해주세요.\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    EnterBattle(me, monsterList);
                    break;
                }
                else if (int.Parse(choice) <= count)
                {
                    if (monsterList[int.Parse(choice)-1].hp > 0)
                    {
                        DealDamage(me,monsterList, int.Parse(choice));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public static void DealDamage(Character me, List<Monster> monsterList,int target)
        {
            Random random = new Random();
            bool isAllKill = true;
            int bonusDamage = 0;
            string? choice = "";
            bonusDamage = (int)me.atk / 10;
            if (me.atk % 10 != 0)
            {
                bonusDamage++;
            }
            int damage = (int)me.atk;
            damage = random.Next(damage - bonusDamage, damage + bonusDamage + 1);
            Monster targetMonster=monsterList[target-1];
            string remainhp="";
            if (targetMonster.hp <= damage)
            {
                remainhp = "Dead";
            }
            else
            {
                remainhp = (targetMonster.hp - damage).ToString();
            }
            Console.Clear();
            Console.Write($"Battle!!\n\n{me.name} 의 공격!\nLv.{targetMonster.level} {targetMonster.name} 을(를) 맞췄습니다. [데미지 : {damage}]\n\n" +
                $"Lv.{targetMonster.level} {targetMonster.name}\nHP {targetMonster.hp} -> {remainhp}\n\n0. 다음\n\n>>");
            if (targetMonster.hp >= damage)
            {
                targetMonster.hp-=damage;
            }
            else
            {
                targetMonster.hp = 0; ;
            }
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    foreach (Monster monster in monsterList)
                    {
                        if (monster.hp > 0)
                        {
                            isAllKill = false;
                        }
                    }
                    if (isAllKill == true)
                    {
                        Victory(me);
                    }
                    else
                    {
                        EnemyBattle(me, monsterList);
                    }
                    
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        public static void EnemyBattle(Character me, List<Monster> monsterList)
        {
            foreach (Monster monster in monsterList)
            {
                string choice = "";
                if (monster.hp > 0)
                {
                    Console.Clear();
                    Console.Write($"Battle!!\n\nLv.{monster.level} {monster.name}의 공격!\n{me.name} 을(를) 맞췄습니다. [데미지 : {monster.atk}]\n\n" +
                        $"Lv.{me.level} {me.name} HP {me.hp} -> {me.hp-monster.atk}\n\n0. 다음\n\n대상을 선택해주세요.\n>>");
                    me.hp -= monster.atk;
                    if (me.hp <= 0)
                    {
                        GameOver(me);
                        break;
                    }
                    else
                    {
                        while (true)
                        {
                            choice = Console.ReadLine();
                            if (choice == "0")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }

                        }
                    }
                }
            }
            if (me.hp > 0) EnterBattle(me,monsterList);
        }

        public static void GameOver(Character me)
        {
            Console.Clear();
            string? choice = "";
            Console.Write($"Battle!! - Result\n\nYou Lose\n\nHP {starthp} -> {me.hp}\n\n0. 게임종료\n\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

            }
        }

        public static void Victory(Character me)
        {
            Console.Clear();
            string? choice = "";
            Console.Write($"Battle!! - Result\n\nVictory\n\nHP {starthp} -> {me.hp}\n\n0. 다음\n\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    Program.GameStart(me);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

            }
        }
    }
}
