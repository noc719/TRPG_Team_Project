using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class Quest
    {
        public string questTitle { get; private set; }
        public bool questClearCheck { get; set; }
        public Quest(string questtitle, bool questclearcheck)
        {
            questTitle = questtitle;
            questClearCheck = questclearcheck;
        }

        public static List<Quest> questList = new List<Quest>();

        public static void QuesScene(Character me)
        {
            //start에 quest 집어넣기
            Console.Clear();
            Console.WriteLine("Quest!!\n");
            QuestAdd();

            Console.WriteLine("1. 퀘스트 선택");
            Console.WriteLine("0. 나가기");

            Console.Write(">>");
            while (true)
            {
                string? choice = Console.ReadLine();
                if (choice == "0")
                {
                    Program.GameStart(me);
                    break;
                }
                else if (int.TryParse(choice, out int choiceNumber) && choice == "1")
                {
                    Console.Clear();
                    for (int i = 0; i < questList.Count; i++)
                    {
                        QuestListOutput(i);
                    }

                    Console.WriteLine("\n");
                    QuestClearCheck(choiceNumber);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
        public static void QuestListOutput(int i)
        {
            Quest quest = questList[i];
            Console.WriteLine($"{i + 1}. {quest.questTitle} - {(quest.questClearCheck ? "완료" : "미완료")}");
            Console.WriteLine("");
        }
        public static void QuestClearCheck(int choiceNumber)
        {
            // 선택된 번호가 유효한지 확인
            if (choiceNumber >= 1 && choiceNumber <= questList.Count)
            {
                var quest = questList[choiceNumber - 1]; // 배열은 0부터 시작하므로 -1

                //"클리어 로직"
                //if (!quest.questClearCheck)
                //{
                //    quest.questClearCheck = true; // 퀘스트 완료
                //    Console.WriteLine($"{quest.questTitle}가 완료되었습니다!");
                //}
                //else
                //{
                //    Console.WriteLine($"{quest.questTitle}는 이미 완료되었습니다.");
                //}
            }
            else
            {
                Console.WriteLine("유효하지 않은 선택입니다.");
            }
        }
        public static void QuestAdd()
        {
            var questTitles = new List<string>
            {
                "마을을 위협하는 미니언 처치",
                "장비를 장착해보자",
                "더욱 더 강해지기!",
                "????????"
            };

            foreach (var title in questTitles)
            {
                if (!questList.Any(q => q.questTitle == title))
                {
                    questList.Add(new Quest(title, false));
                }
            }
        }
    }
}
