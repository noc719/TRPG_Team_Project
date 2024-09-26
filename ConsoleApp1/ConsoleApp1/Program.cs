
using System.Dynamic;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Linq;

namespace ConsoleApp1
{
    
    internal class Program
    {
        public static ItemList? itemlist;
        static void Main(string[] args)
        {
            int a=0;
            string? choice;
            itemlist = new ItemList();
            if (File.Exists(".\\data.dat"))//저장된 데이터 있는지 확인해서 불러오거나 새로 시작
            {
                Console.Write($"저장된 데이터가 있습니다.\n불러오시겠습니까?\n\n1.불러오기\n2.새로 시작\n\n원하시는 행동을 입력해주세요.\n>>");
                while (true)
                {
                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Character me = new Character("");
                        SaveAndLoad.LoadData(me, itemlist);
                        GameStart(me);
                        a = 1;
                        break;
                    }
                    else if (choice == "2")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    }
                }
            }
            

            while (a==0)//데이터가 없거나 새로 시작하는 경우 이름 결정
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 결정해주세요.");
                string? name = Console.ReadLine();
                Console.Write($"입력하신 이름은 {name}입니다.\n\n1.저장\n2.취소\n\n원하시는 행동을 입력해주세요.\n>>");
                while (true)
                {
                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        a = 1;
                        Character me = new Character(name);
                        GameStart(me);
                        break;
                    }
                    else if (choice == "2")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    }
                }
            }
        }
        public static void GameStart(Character me)//메인 화면
        {
            int a = 0;
            Console.Clear();
            Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n" +
                "1. 상태보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식\n6. 저장\n0. 게임종료\n\n원하시는 행동을 입력해주세요.\n>>");
            while (a==0)
            {
                string? choice = Console.ReadLine();
                if (choice == "1")
                {
                    Status.EnterStatus(me);
                    a = 1;
                }
                else if (choice == "2")
                {
                    Inventory.EnterInventory(me);
                    a = 1;
                }
                else if(choice == "3")
                {
                    Shop.EnterShop(me);
                    a = 1;
                }
                else if (choice == "4")
                {
                    Dungeon.EnterDungeon(me);
                    a = 1;
                }
                else if (choice == "5")
                {
                    Rest.EnterRest(me);
                    a = 1;
                }
                else if (choice == "6")
                {
                    SaveAndLoad.SaveData(me,itemlist);
                    Console.Write("\n저장되었습니다.\n>> ");
                }
                else if (choice == "0")
                {
                    a = 1;
                    Console.WriteLine("\n게임을 종료합니다.\n ");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
    }
}
