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
                    // 죽으면 퀘스트
                    if (monster.name == "미니언")
                    {
                        me.questMinionKill++;
                    }else if(monster.name == "대포미니언")
                    {
                        me.questMaxionKill++;
                    }
                    else if (monster.name == "공허충")
                    {
                        me.questVoidBugKill++;
                    }
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.maxhp}/{me.hp}\nMP {me.maxmp} / {me.mp}\n\n1. 공격\n2. 스킬\n\n원하시는 행동을 입력해주세요.\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    Attack(me, monsterlist);
                    break;
                }
                if (choice == "2")
                {
                    Skill(me, monsterlist);
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
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.maxhp}/{me.hp}\nMP {me.maxmp} / {me.mp}\n\n");
            Console.Write("0. 취소\n\n대상을 선택해주세요.\n>>");
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
                    if (monsterList[int.Parse(choice) - 1].hp > 0)
                    {
                        DealDamage(me, monsterList, int.Parse(choice));
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

        public static void Skill(Character me, List<Monster> monsterList)
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
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.maxhp}/{me.hp}\nMP {me.maxmp} / {me.mp}\n\n");
            if (me.job == "전사")
            {
                Console.Write($"1. 알파 스트라이크 - MP 10\n   공격력 * 2 로 하나의 적을 공격합니다.\n\n" +
                    $"2. 더블 스트라이크 - MP 15\n   공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n\n");
            }
            else if (me.job == "도적")
            {
                Console.Write($"1. 기습 - MP 10\n   공격력 * 0.5 로 하나의 적을 공격합니다. 이후 추가 턴을 가집니다.\n\n" +
                    $"2. 칼날 부채 - MP 15\n   공격력 * 0.8 로 모든 적을 공격합니다.\n\n");
            }
            Console.Write("0. 취소\n\n원하시는 행동을 입력해주세요.\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    EnterBattle(me, monsterList);
                    break;
                }
                else if (choice == "1")
                {
                    if (me.mp >= 10)
                    {
                        if (me.job == "전사")
                        {
                            me.mp -= 10;
                            AlphaStrike(me, monsterList);
                            break;
                        }
                        else if (me.job == "도적")
                        {
                            me.mp -= 10;
                            Ambush(me, monsterList);
                            break;
                        }
                    }
                    else
                    {
                        Console.Write("MP가 부족합니다.\n>>");
                    }
                }
                else if (choice == "2")
                {
                    if (me.mp >= 15)
                    {
                        if (me.job == "전사")
                        {
                            me.mp -= 15;
                            DoubleStrike(me, monsterList);
                            break;
                        }
                        else if (me.job == "도적")
                        {
                            me.mp -= 15;
                            FanOfBlade(me, monsterList);
                            break;
                        }
                    }
                    else
                    {
                        Console.Write("MP가 부족합니다.\n>>");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public static void DealDamage(Character me, List<Monster> monsterList, int target)
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
            Monster targetMonster = monsterList[target - 1];
            string remainhp = "";
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
                targetMonster.hp -= damage;
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
                        $"Lv.{me.level} {me.name} HP {me.hp} -> {me.hp - monster.atk}\n\n0. 다음\n\n대상을 선택해주세요.\n>>");
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
            if (me.hp > 0) EnterBattle(me, monsterList);
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
            Console.Write($"Battle!! - Result\n\nVictory\n\nHP {starthp} -> {me.hp}\nMP 10 회복됨\n\n0. 다음\n\n>>");
            if (me.mp >= 40)
            {
                me.mp = 50;
            }
            else
            {
                me.mp += 10;
            }
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

        public static void AlphaStrike(Character me, List<Monster> monsterList)
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
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.maxhp}/{me.hp}\nMP {me.maxmp} / {me.mp}\n\n");
            Console.Write("0. 취소\n\n대상을 선택해주세요.\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    Skill(me, monsterList);
                    break;
                }
                else if (int.Parse(choice) <= count)
                {
                    if (monsterList[int.Parse(choice) - 1].hp > 0)
                    {
                        AlphaStrikeDealDamage(me, monsterList, int.Parse(choice));
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
        public static void AlphaStrikeDealDamage(Character me, List<Monster> monsterList, int target)
        {
            bool isAllKill = true;
            string? choice = "";
            int damage = (int)(me.atk * 2);
            Monster targetMonster = monsterList[target - 1];
            string remainhp = "";
            if (targetMonster.hp <= damage)
            {
                remainhp = "Dead";
            }
            else
            {
                remainhp = (targetMonster.hp - damage).ToString();
            }
            Console.Clear();
            Console.Write($"Battle!!\n\n{me.name} 의 알파 스트라이크!\nLv.{targetMonster.level} {targetMonster.name} 을(를) 맞췄습니다. [데미지 : {damage}]\n\n" +
                $"Lv.{targetMonster.level} {targetMonster.name}\nHP {targetMonster.hp} -> {remainhp}\n\n0. 다음\n\n>>");
            if (targetMonster.hp >= damage)
            {
                targetMonster.hp -= damage;
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

        public static void DoubleStrike(Character me, List<Monster> monsterList)
        {
            Random random = new Random();
            List<Monster> temp = new List<Monster>();
            foreach (Monster monster in monsterList)
            {
                if (monster.hp > 0)
                {
                    temp.Add(monster);
                }
            }
            bool isAllKill = true;
            string? choice = "";
            int damage = (int)(me.atk * 1.5);
            int effectedCount = 0;

            Monster targetMonster1 = null;
            Monster targetMonster2 = null;

            if (temp.Count >= 2)
            {
                targetMonster1 = temp[random.Next(0, temp.Count)];
                temp.Remove(targetMonster1);
                targetMonster2 = temp[random.Next(0, temp.Count)];
                effectedCount = 2;
            }
            else
            {
                targetMonster1 = temp[0];
                effectedCount = 1;
            }
            string remainhp1 = "";
            string remainhp2 = "";

            if (targetMonster1.hp <= damage)
            {
                remainhp1 = "Dead";
            }
            else
            {
                remainhp1 = (targetMonster1.hp - damage).ToString();
            }

            if (effectedCount == 2)
            {
                if (targetMonster2.hp <= damage)
                {
                    remainhp2 = "Dead";
                }
                else
                {
                    remainhp2 = (targetMonster2.hp - damage).ToString();
                }
            }

            Console.Clear();
            Console.Write($"Battle!!\n\n{me.name} 의 더블 스트라이크!\n");
            Console.Write($"Lv.{targetMonster1.level} {targetMonster1.name} ");
            if (effectedCount == 2)
            {
                Console.Write($"Lv.{targetMonster2.level} {targetMonster2.name} ");
            }
            Console.Write($"을(를) 맞췄습니다. [데미지 : {damage}]\n\n");
            Console.WriteLine($"Lv.{targetMonster1.level} {targetMonster1.name}\nHP {targetMonster1.hp} -> {remainhp1}");
            if (effectedCount == 2)
            {
                Console.WriteLine($"Lv.{targetMonster2.level} {targetMonster2.name}\nHP {targetMonster2.hp} -> {remainhp2}");
            }
            Console.Write($"\n0. 다음\n\n>>");
            if (targetMonster1.hp >= damage)
            {
                targetMonster1.hp -= damage;
            }
            else
            {
                targetMonster1.hp = 0; ;
            }
            if (effectedCount == 2)
            {
                if (targetMonster2.hp >= damage)
                {
                    targetMonster2.hp -= damage;
                }
                else
                {
                    targetMonster2.hp = 0; ;
                }
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
        public static void Ambush(Character me, List<Monster> monsterList)
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
            Console.Write($"\n\n[내정보]\nLv.{me.level} {me.name} ({me.job})\nHP {me.maxhp}/{me.hp}\nMP {me.maxmp} / {me.mp}\n\n");
            Console.Write("0. 취소\n\n대상을 선택해주세요.\n>>");
            while (true)
            {
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    Skill(me, monsterList);
                    break;
                }
                else if (int.Parse(choice) <= count)
                {
                    if (monsterList[int.Parse(choice) - 1].hp > 0)
                    {
                        AmbushDealDamage(me, monsterList, int.Parse(choice));
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
        public static void AmbushDealDamage(Character me, List<Monster> monsterList, int target)
        {
            bool isAllKill = true;
            string? choice = "";
            int damage = (int)(me.atk * 0.5);
            Monster targetMonster = monsterList[target - 1];
            string remainhp = "";
            if (targetMonster.hp <= damage)
            {
                remainhp = "Dead";
            }
            else
            {
                remainhp = (targetMonster.hp - damage).ToString();
            }
            Console.Clear();
            Console.Write($"Battle!!\n\n{me.name} 의 기습!\n");
            Console.Write($"Lv.{targetMonster.level} {targetMonster.name} ");
            Console.Write($"을(를) 맞췄습니다. [데미지 : {damage}]\n\n");
            Console.Write($"Lv.{targetMonster.level} {targetMonster.name}\nHP {targetMonster.hp} -> {remainhp}\n");
            Console.Write($"\n0. 다음\n\n>>");
            if (targetMonster.hp >= damage)
            {
                targetMonster.hp -= damage;
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
                        EnterBattle(me, monsterList);
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        public static void FanOfBlade(Character me, List<Monster> monsterList)
        {
            bool isAllKill = true;
            string? choice = "";
            int damage = (int)(me.atk * 0.8);
            string remainhp = "";
            List<Monster> temp = new List<Monster>();
            foreach (Monster monster in monsterList)
            {
                if (monster.hp > 0)
                {
                    temp.Add(monster);
                }
            }
            Console.Clear();
            Console.Write($"Battle!!\n\n{me.name} 의 칼날 부채!\n");
            foreach(Monster monster in temp)
            {
                Console.Write($"Lv.{monster.level} {monster.name} ");
            }
            Console.Write($"을(를) 맞췄습니다. [데미지 : {damage}]\n\n");
            foreach (Monster monster in temp)
            {
                if (monster.hp <= damage)
                {
                    remainhp = "Dead";
                }
                else
                {
                    remainhp = (monster.hp - damage).ToString();
                }
                Console.Write($"Lv.{monster.level} {monster.name}\nHP {monster.hp} -> {remainhp}\n");
            }
            Console.Write($"\n0. 다음\n\n>>");
            foreach (Monster monster in temp)
            {
                if (monster.hp >= damage)
                {
                    monster.hp -= damage;
                }
                else
                {
                    monster.hp = 0; ;
                }
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
    }
}
