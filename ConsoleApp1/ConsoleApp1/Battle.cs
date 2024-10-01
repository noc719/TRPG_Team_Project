using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Battle
    {
        //초기 변수값
        public static int starthp;
        public static int startLevel;
        public static int startExp;
        //보상 저장소
        public static int saveGold = 0;
        public static List<Potion> pickupPotion = new List<Potion>();     
        public static List<item> pickupItem = new List<item>();
        public static int saveExp = 0;
        private static List<Quest> battlequestlist = Program.questlist.quests;


        public static void EnterBattle(Character me, List<Monster> monsters)
        {
            Console.Clear();
            string? choice = "";
            List<Monster> monsterlist = new List<Monster>();
            if (monsters == null)
            {
                starthp = me.hp;
                startLevel = me.level;
                startExp = me.exp;
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
            Console.Write($"\n\n[내정보]\n\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\nMP {me.mp} / {me.maxmp}\n\n1. 공격\n2. 스킬\n\n원하시는 행동을 입력해주세요.\n>>");
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
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  HP{monster.hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  Dead");
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\n\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\nMP {me.mp} / {me.maxmp}\n\n");
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
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  HP{monster.hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  Dead");
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\n\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\nMP {me.mp} / {me.maxmp}\n\n");
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
            int evade=0;
            evade=random.Next(1,11);
            int critical = 0;
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
            Console.Clear();
            if (evade == 1)
            {
                Console.Write($"Battle!!\n\n{me.name} 의 공격!\nLv.{targetMonster.level} {targetMonster.name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n\n0. 다음\n\n>>");
            }
            else
            {
                critical = random.Next(1, 101);
                if (critical < 16)
                {
                    damage = (int)(damage * 1.6);
                }
                if (targetMonster.hp <= damage)
                {
                    remainhp = "Dead";
                    targetMonster.Reward(me);
                    ////////////////////////////////////////////////////////////
                    /// 몬스터 사망시 죽은 몬스터 이름에 따라 퀘스트 진행도 증가
                    /////////////////////////////////////////////////////////////
                    foreach (var quest in battlequestlist)
                    {
                        if (targetMonster.name == "미니언" && me.questMinionKill < 5 && quest.questTitle == "마을을 위협하는 미니언 처치" && quest.isQuestAccepted)  // 퀘스트가 미니언 처치 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                        {
                            me.questMinionKill++;
                            quest.progressCount = me.questMinionKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                        else if (targetMonster.name == "대포미니언" && me.questMaxionKill < 5 && quest.questTitle == "마을을 위협하는 대포 미니언 처치" && quest.isQuestAccepted)
                        {
                            me.questMaxionKill++;
                            quest.progressCount = me.questMaxionKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                        else if (targetMonster.name == "공허충" && me.questVoidBugKill < 5 && quest.questTitle == "마을을 위협하는 공허충 처치 이브" && quest.isQuestAccepted)
                        {
                            me.questVoidBugKill++;
                            quest.progressCount = me.questVoidBugKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                    }
                }
                else
                {
                    remainhp = (targetMonster.hp - damage).ToString();
                }
                Console.Write($"Battle!!\n\n{me.name} 의 공격!\nLv.{targetMonster.level} {targetMonster.name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                if (critical < 16) { Console.WriteLine(" - 치명타 공격!!"); }
                Console.Write($"\n\nLv.{targetMonster.level} {targetMonster.name}\nHP {targetMonster.hp} -> {remainhp}\n\n0. 다음\n\n>>");
                if (targetMonster.hp >= damage)
                {
                    targetMonster.hp -= damage;
                }
                else
                {
                    targetMonster.hp = 0; ;
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
            Console.Write($"Battle!! - Result\n\nVictory\n\n");
            ClearReward(me);
            Console.Write("0. 다음\n\n>>");
           
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
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  HP{monster.hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  Dead");
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\n\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\nMP {me.mp} / {me.maxmp}\n\n");
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
                targetMonster.Reward(me);
                ////////////////////////////////////////////////////////////
                /// 몬스터 사망시 죽은 몬스터 이름에 따라 퀘스트 진행도 증가
                /////////////////////////////////////////////////////////////
                foreach (var quest in battlequestlist)
                {
                    if (targetMonster.name == "미니언" && me.questMinionKill < 5 && quest.questTitle == "마을을 위협하는 미니언 처치" && quest.isQuestAccepted)  // 퀘스트가 미니언 처치 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                    {
                        me.questMinionKill++;
                        quest.progressCount = me.questMinionKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                    else if (targetMonster.name == "대포미니언" && me.questMaxionKill < 5 && quest.questTitle == "마을을 위협하는 대포 미니언 처치" && quest.isQuestAccepted)
                    {
                        me.questMaxionKill++;
                        quest.progressCount = me.questMaxionKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                    else if (targetMonster.name == "공허충" && me.questVoidBugKill < 5 && quest.questTitle == "마을을 위협하는 공허충 처치 이브" && quest.isQuestAccepted)
                    {
                        me.questVoidBugKill++;
                        quest.progressCount = me.questVoidBugKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                }
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
                targetMonster1.Reward(me);
                ////////////////////////////////////////////////////////////
                /// 몬스터 사망시 죽은 몬스터 이름에 따라 퀘스트 진행도 증가
                /////////////////////////////////////////////////////////////
                foreach (var quest in battlequestlist)
                {
                    if (targetMonster1.name == "미니언" && me.questMinionKill < 5 && quest.questTitle == "마을을 위협하는 미니언 처치" && quest.isQuestAccepted)  // 퀘스트가 미니언 처치 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                    {
                        me.questMinionKill++;
                        quest.progressCount = me.questMinionKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                    else if (targetMonster1.name == "대포미니언" && me.questMaxionKill < 5 && quest.questTitle == "마을을 위협하는 대포 미니언 처치" && quest.isQuestAccepted)
                    {
                        me.questMaxionKill++;
                        quest.progressCount = me.questMaxionKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                    else if (targetMonster1.name == "공허충" && me.questVoidBugKill < 5 && quest.questTitle == "마을을 위협하는 공허충 처치 이브" && quest.isQuestAccepted)
                    {
                        me.questVoidBugKill++;
                        quest.progressCount = me.questVoidBugKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                }
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
                    targetMonster2.Reward(me);
                    ////////////////////////////////////////////////////////////
                    /// 몬스터 사망시 죽은 몬스터 이름에 따라 퀘스트 진행도 증가
                    /////////////////////////////////////////////////////////////
                    foreach (var quest in battlequestlist)
                    {
                        if (targetMonster2.name == "미니언" && me.questMinionKill < 5 && quest.questTitle == "마을을 위협하는 미니언 처치" && quest.isQuestAccepted)  // 퀘스트가 미니언 처치 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                        {
                            me.questMinionKill++;
                            quest.progressCount = me.questMinionKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                        else if (targetMonster2.name == "대포미니언" && me.questMaxionKill < 5 && quest.questTitle == "마을을 위협하는 대포 미니언 처치" && quest.isQuestAccepted)
                        {
                            me.questMaxionKill++;
                            quest.progressCount = me.questMaxionKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                        else if (targetMonster2.name == "공허충" && me.questVoidBugKill < 5 && quest.questTitle == "마을을 위협하는 공허충 처치 이브" && quest.isQuestAccepted)
                        {
                            me.questVoidBugKill++;
                            quest.progressCount = me.questVoidBugKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                    }
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
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  HP{monster.hp}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{count}. Lv.{monster.level} {monster.name}  Dead");
                    Console.ResetColor();
                }
            }
            Console.Write($"\n\n[내정보]\n\nLv.{me.level} {me.name} ({me.job})\nHP {me.hp}/{me.maxhp}\nMP {me.mp} / {me.maxmp}\n\n");
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
                targetMonster.Reward(me);
                ////////////////////////////////////////////////////////////
                /// 몬스터 사망시 죽은 몬스터 이름에 따라 퀘스트 진행도 증가
                /////////////////////////////////////////////////////////////
                foreach (var quest in battlequestlist)
                {
                    if (targetMonster.name == "미니언" && me.questMinionKill < 5 && quest.questTitle == "마을을 위협하는 미니언 처치" && quest.isQuestAccepted)  // 퀘스트가 미니언 처치 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                    {
                        me.questMinionKill++;
                        quest.progressCount = me.questMinionKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                    else if (targetMonster.name == "대포미니언" && me.questMaxionKill < 5 && quest.questTitle == "마을을 위협하는 대포 미니언 처치" && quest.isQuestAccepted)
                    {
                        me.questMaxionKill++;
                        quest.progressCount = me.questMaxionKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                    else if (targetMonster.name == "공허충" && me.questVoidBugKill < 5 && quest.questTitle == "마을을 위협하는 공허충 처치 이브" && quest.isQuestAccepted)
                    {
                        me.questVoidBugKill++;
                        quest.progressCount = me.questVoidBugKill;
                        break; // 퀘스트 처리 후 반복문 종료
                    }
                }
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
                    monster.Reward(me);
                    ////////////////////////////////////////////////////////////
                    /// 몬스터 사망시 죽은 몬스터 이름에 따라 퀘스트 진행도 증가
                    /////////////////////////////////////////////////////////////
                    foreach (var quest in battlequestlist)
                    {                      
                        if (monster.name == "미니언" && me.questMinionKill < 5 && quest.questTitle == "마을을 위협하는 미니언 처치" && quest.isQuestAccepted)  // 퀘스트가 미니언 처치 퀘스트인지 확인하고, 해당 퀘스트가 수락된 상태일 때
                        {
                            me.questMinionKill++;
                            quest.progressCount = me.questMinionKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                        else if (monster.name == "대포미니언" && me.questMaxionKill < 5 && quest.questTitle == "마을을 위협하는 대포 미니언 처치" && quest.isQuestAccepted)
                        {
                            me.questMaxionKill++;
                            quest.progressCount = me.questMaxionKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                        else if (monster.name == "공허충" && me.questVoidBugKill < 5 && quest.questTitle == "마을을 위협하는 공허충 처치 이브" && quest.isQuestAccepted)
                        {
                            me.questVoidBugKill++;
                            quest.progressCount = me.questVoidBugKill;
                            break; // 퀘스트 처리 후 반복문 종료
                        }
                    }
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


        public static void ClearReward(Character me)
        {
            me.exp += Battle.saveExp; //levelsystem
            me.LevelSystem();

            Console.WriteLine("[캐릭터 정보]");

            Console.Write($"Lv.{startLevel} {me.name} ->"); //레벨
            Console.WriteLine($" Lv.{me.level} {me.name}");

            Console.WriteLine($"HP { starthp} -> { me.hp}");  //hp
            Console.WriteLine("MP 10 회복됨");

            Console.Write("exp {0} -> ", startExp);  //exp
            Console.WriteLine("{0}", me.exp);

            Console.WriteLine();

            Console.WriteLine("[획득 아이템]");
            foreach (item item in Battle.pickupItem)
            {
                if (item.isBought == true) //중복된 장비일 경우
                {
                    Console.WriteLine("중복 장비 획득 - {0}G", item.price / 3);  //장비 원값의 3분의 1 가격을 얻음
                    Battle.saveGold += item.price / 3;
                }
                else
                {
                    item.isBought = true;
                    me.inventory.Add(item);
                    Console.WriteLine("{0} - 1개", item.name);
                }
            }

            Console.WriteLine("획득한 골드 - {0}G", Battle.saveGold);   //획득 골드 부분
            me.gold += Battle.saveGold;

            foreach (Potion dropedPotion in Battle.pickupPotion)
            {

                PotionInventory.AddPotionToInventory(dropedPotion, me);
            }

            Console.WriteLine("획득한 포션 - {0} 개", Battle.pickupPotion.Count);


            saveGold = 0;
            saveExp = 0;
            pickupPotion.Clear();
            pickupItem.Clear();

        }




    }
}

