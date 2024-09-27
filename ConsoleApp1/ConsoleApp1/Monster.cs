using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Monster
    {

        public int level;
        public string name;
        public int hp;
        public int def;
        public int atk;


        //몬스터 정보
        //몬스터 스폰을 랜덤하게
        public Monster(int level, string name, int hp, int def, int atk)
        {

            this.level = level;
            this.name = name;
            this.hp = hp;
            this.def = def;
            this.atk = atk;
            Console.WriteLine($"Lv.{level} {name}  HP {hp}");


        }

        public void MonsterAttack()
        {
            Console.WriteLine($"Lv.{level} {name} 의 공격!");

            //(플레이어 이름)을 맞췄습니다. [플레이어의 방어력에 비례한 피해!]


        }




    }

    public class Minion : Monster
    {

        public Minion() : base(2, "미니언", 15, 10, 5)
        {

        }

    }

    public class VoidInsec : Monster
    {

        public VoidInsec() : base(3, "공허충", 10, 10, 9)
        {

        }

    }
    public class CannonMinion : Monster
    {

        public CannonMinion() : base(5, "대포미니언", 25, 10, 8)
        {

        }

    }



}












