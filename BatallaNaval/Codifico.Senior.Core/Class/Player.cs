using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codifico.Senior.Core.Class
{
    public class Player
    {
        int[,] battleTable = new int[8, 8];

        public int[,] BattleTable
        {
            get { return battleTable; }
            set { battleTable = value; }
        }
        public string Name { get; set; }
        public string Host { get; set; }
        public bool Turn { get; set; }
    }
}
