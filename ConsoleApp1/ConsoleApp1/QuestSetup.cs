using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // -----------------------------------------------------------------
    //| 처치한 몬스터는 저장 되는데 퀘스트 리스트가 저장 안 됨 수정하기 |
    // -----------------------------------------------------------------
    internal class QuestSetup
    {
        private static bool isQuestAdded = false;
        private static List<Quest> questList = QuestList.questList;
        // 퀘스트 씬 출력
        public static void QuestScene(Character me) // 퀘스트 씬
        {
            Console.Clear(); // 화면 초기화
            Console.WriteLine("Quest!!\n"); // 퀘스트 씬 제목 출력
           
            if (!isQuestAdded)//가장 처음 시작할때 퀘스트 리스트 받기
            {
                QuestList.QuestAdd();
                Console.WriteLine("퀘스트 추가 중");
                isQuestAdded = true;
            }
            // 선택 옵션 출력
            Console.WriteLine("1. 수락한 퀘스트");
            Console.WriteLine("2. 수락하지 않은 퀘스트");
            Console.WriteLine("0. 나가기\n");
            Console.Write(">>");
            // 사용자 입력 대기
            while (true)
            {
                string? choice = Console.ReadLine(); // 사용자 입력 받기
                if (choice == "0") // 마을로 돌아가기
                {
                    Program.GameStart(me); // 게임 시작 메서드 호출
                    break;
                }
                else if (choice == "1") // 수락한 퀘스트 선택
                {
                    while (true)
                    {
                        Console.Clear(); // 화면 초기화
                        for (int i = 0; i < questList.Count; i++) // 퀘스트 리스트 출력
                        {
                            NotAcceptedQuestListOutput(i); // 수락하지 않은 퀘스트 출력
                        }
                        Console.WriteLine("\n0. 마을로 돌아가기");
                        Console.WriteLine("\n원하시는 퀘스트를 선택해주세요");
                        Console.Write(">>");
                        string? questchoice = Console.ReadLine(); // 퀘스트 선택 받기
                        if (int.TryParse(questchoice, out int questchoiceNumber)) // 숫자로 변환
                        {
                            if (questchoiceNumber == 0) // 마을로 돌아가기 선택
                            {
                                Program.GameStart(me);
                            }
                            else
                            {
                                Console.WriteLine("\n"); QuestChoiceScene(me, questchoiceNumber); // 선택한 퀘스트 씬으로 이동
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요."); // 유효하지 않은 입력
                        }
                        break;
                    }
                }
                else if (choice == "2") // 수락하지 않은 퀘스트 선택
                {
                    while (true)
                    {
                        Console.Clear(); // 화면 초기화
                        for (int i = 0; i < questList.Count; i++) // 퀘스트 리스트 출력
                        {
                            AcceptedQuestListOutput(i); // 수락한 퀘스트 출력
                        }
                        Console.WriteLine("\n0. 마을로 돌아가기");
                        Console.WriteLine("\n원하시는 퀘스트를 선택해주세요");
                        Console.Write(">>");
                        string? questchoice = Console.ReadLine(); // 퀘스트 선택 받기
                        if (int.TryParse(questchoice, out int questchoiceNumber)) // 숫자로 변환
                        {
                            if (questchoiceNumber == 0) // 마을로 돌아가기 선택
                            {
                                Program.GameStart(me);
                            }
                            else
                            {
                                Console.WriteLine("\n"); QuestChoiceScene(me, questchoiceNumber); // 선택한 퀘스트 씬으로 이동
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요."); // 유효하지 않은 입력
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요."); // 유효하지 않은 입력
                }
            }
        }

        // 퀘스트 리스트 출력
        public static void QuestListOutput(int i, bool isAccepted)
        {
            // 주어진 인덱스 i에 해당하는 퀘스트를 가져온다.
            Quest quest = questList[i];
            if (quest.isQuestAccepted == isAccepted)
            {
                // 퀘스트 제목과 클리어 상태(완료 또는 미완료)를 출력한다.
                Console.WriteLine($"{i + 1}. {quest.questTitle} - {(quest.questClearCheck ? "완료" : "미완료")}");
            }
        }

        // 수락하지 않은 퀘스트 목록을 출력하는 함수
        public static void NotAcceptedQuestListOutput(int i)
        {
            QuestListOutput(i, false); // isAccepted 인자를 false로 설정하여 호출
        }

        // 수락한 퀘스트 목록을 출력하는 함수
        public static void AcceptedQuestListOutput(int i)
        {
            QuestListOutput(i, true); // isAccepted 인자를 true로 설정하여 호출
        }
        // 선택한 퀘스트 씬을 출력하는 메서드
        public static void QuestChoiceScene(Character me, int choiceNumber)
        {
            Console.Clear(); // 화면 초기화
            var quest = questList[choiceNumber - 1]; // 배열은 0부터 시작하므로 -1
            foreach (var questcheck in questList)
            {
                if (questcheck.questTitle.Contains("미니언"))
                {
                    questcheck.progressCount = me.questMinionKill;
                }else if (questcheck.questTitle.Contains("대포미니언"))
                {
                    questcheck.progressCount = me.questMaxionKill;
                }else if (questcheck.questTitle.Contains("공허충"))
                {
                    questcheck.progressCount = me.questVoidBugKill;
                }
            }
            while (true) // 반복문 시작
            {
                Console.Clear();
                if (quest.isQuestAccepted && quest.progressCount >= quest.requiredCount)
                {
                    Console.Write($"Quest!!\r\n\r\n{quest.questTitle}\r\n\r\n너같은 뉴비한테는 이 퀘스트가 딱이야\r\n\r\n\r\n- {quest.questTitle}. {quest.progressCount} / {quest.requiredCount}\r\n\r\n- 보상- \n{quest.rewardP} G\r\n\r\n1. 보상 받기\r\n0. 마을로 돌아가기\r\n원하시는 행동을 입력해주세요.\r\n>>");
                }
                else if (quest.isQuestAccepted)
                {
                    Console.Write($"Quest!!\r\n\r\n{quest.questTitle}\r\n\r\n너같은 뉴비한테는 이 퀘스트가 딱이야\r\n\r\n\r\n- {quest.questTitle}. {quest.progressCount} / {quest.requiredCount}\r\n\r\n- 보상- \n{quest.rewardP} G\r\n\r\n0. 마을로 돌아가기\r\n원하시는 행동을 입력해주세요.\r\n>>");
                }
                else
                {
                    Console.Write($"Quest!!\r\n\r\n{quest.questTitle}\r\n\r\n너같은 뉴비한테는 이 퀘스트가 딱이야\r\n\r\n\r\n- {quest.questTitle}. {quest.progressCount} / {quest.requiredCount}\r\n\r\n- 보상- \n{quest.rewardP} G\r\n\r\n1. 수락\r\n0. 마을로 돌아가기\r\n원하시는 행동을 입력해주세요.\r\n>>");
                }

                string? input = Console.ReadLine(); // 사용자 입력 받기

                if (input == "0") // 마을로 돌아가기 선택
                {
                    Program.GameStart(me); // 게임 시작 메서드 호출
                    break; // 반복문 종료
                }
                else if (input == "1" && !quest.isQuestAccepted) // 퀘스트 수락
                {
                    quest.isQuestAccepted = true; // 퀘스트 수락 상태 변경
                    Console.WriteLine($"{quest.questTitle} 퀘스트를 수락하였습니다!"); // 수락 메시지
                    Console.WriteLine("0번을 눌러서 돌아가세요."); // 돌아가기 안내 메시지
                    string? returnInput = Console.ReadLine(); // 사용자 입력 대기
                    if (returnInput == "0") break; // 0번을 누르면 반복문 종료
                }
                else if (input == "1" && quest.isQuestAccepted && quest.progressCount >= quest.requiredCount) // 보상 수령
                {
                    Console.WriteLine($"보상으로 {quest.rewardP} G를 받았습니다."); // 보상 메시지
                    Console.WriteLine("0번을 눌러서 돌아가세요."); // 돌아가기 안내 메시지
                    string? returnInput = Console.ReadLine(); // 사용자 입력 대기
                    if (returnInput == "0") break; // 0번을 누르면 반복문 종료
                }
                else if (input == "1" && quest.isQuestAccepted) // 퀘스트 미완료 상태
                {
                    Console.WriteLine($"아직 {quest.requiredCount - quest.progressCount} 회가 남았습니다"); // 미완료 메시지
                    Console.WriteLine("0번을 눌러서 돌아가세요."); // 돌아가기 안내 메시지
                    string? returnInput = Console.ReadLine(); // 사용자 입력 대기
                    if (returnInput == "0") break; // 0번을 누르면 반복문 종료
                }
                else // 유효하지 않은 입력
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요."); // 유효하지 않은 입력 메시지
                }
            }
        }
    }
}
