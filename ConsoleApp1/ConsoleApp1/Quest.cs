using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    public class Quest
    {
        public static List<Quest> quest = new List<Quest>();  // 퀘스트 목록 첫 생성
        // 퀘스트 제목
        public string questTitle { get; private set; }
        // 퀘스트 수락 여부
        public bool isQuestAccepted { get; set; } = false;
        // 퀘스트 클리어 여부
        public bool questClearCheck { get; set; } = false;
        // 진행 횟수
        public int progressCount { get; set; }
        // 클리어에 필요한 횟수
        public int requiredCount { get; set; }
        public int rewardP { get; set; }

        // 생성자
        public Quest(string questtitle, bool questclearcheck, int progresscount, int requiredcount, bool isquestaccepted, int rewardp)
        {
            questTitle = questtitle; // 퀘스트 제목 초기화
            questClearCheck = questclearcheck; // 클리어 체크 초기화
            progressCount = progresscount; // 진행 횟수 초기화
            requiredCount = requiredcount; // 필요한 횟수 초기화
            isQuestAccepted = isquestaccepted; // 수락 여부 초기화
            rewardP = rewardp;
        }
    }
}
