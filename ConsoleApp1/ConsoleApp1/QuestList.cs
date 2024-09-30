using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    public class QuestList
    {
        public static List<Quest> questList = new List<Quest>();

        // 퀘스트를 추가하는 메서드
        public static void QuestAdd(Character me)
        {
            // 퀘스트 데이터 목록
            var questData = new List<(string title, bool qusetcheck, int porgress, int required, bool isacceped, int reward)>
            {
                ($"마을을 위협하는 미니언 처치", false, 0, 5, false, 100),
                ($"마을을 위협하는 공허충 처치 이브", false, 0, 5, false, 200),
                ($"마을을 위협하는 대포 미니언 처치", false , 0 , 5 , false, 300),
                ($"장비 장착하기", false , 0 , 1 , false, 50),
                ($"상점 구매하기", false, 0, 1, false, 50)
            };

            // 퀘스트 목록에 추가
            foreach (var (title, qusetcheck, progress, required, isaccepted, rewards) in questData)
            {
                if (!questList.Any(q => q.questTitle == title)) // 중복 체크
                {
                    questList.Add(new Quest(title, qusetcheck, progress, required, isaccepted, rewards)); // 퀘스트 추가
                }
            }
        }

    }
}
