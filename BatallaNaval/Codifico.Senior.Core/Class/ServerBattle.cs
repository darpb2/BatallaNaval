using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codifico.Senior.Core.Class
{
    public static class ServerBattle
    {
        public static List<Player> User = new List<Player>();
        public static List<Game> Gamers = new List<Game>();

        public static int Attack(Point pos, string attacker, string enemy)
        {
            var user = User.First(x => x.Name == attacker);

            if(user.Turn)
            {
                var user2 = User.First(x => x.Name == enemy);
                user.Turn = false;
                user2.Turn = true;

                switch(user2.BattleTable[pos.X, pos.Y])
                {
                    case 0:
                        user2.BattleTable[pos.X, pos.Y] = 3;
                        return user2.BattleTable[pos.X, pos.Y];

                    case 1:
                        user2.BattleTable[pos.X, pos.Y] = 2;
                        return user2.BattleTable[pos.X, pos.Y];
                    default:
                        break;
                }
            }

            return -1;
        }

        public static int[,] getStatus(string player)
        {
            return User.First(x => x.Name == player).BattleTable ?? null;
        }

        public static string getTurn()
        {
            return User.First(x => x.Turn).Name ?? string.Empty;
        }

        public static List<string> getUsers()
        {
            return User.Select( x => x.Name ).ToList();
        }

        public static bool ReceiveAttack(string player)
        {
            return player.Equals("");
        }

        public static bool RegisterUser(Player player)
        {
            if (User.Any(x => x.Name == player.Name))
                return false;

            StartTable(player.BattleTable);
            LocateShipsRandom(4, player.BattleTable);
            User.Add(player);

            return true;
        }

        private static void StartTable(int[,] battleTable)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    battleTable[i, j] = 0;
                }
            }
        }

        public static bool RegisterGame(Game player)
        {
            if (Gamers.Any(x => x.Player1.Equals(player.Player1)
             || x.Player1.Equals(player.Player2)
             || x.Player2.Equals(player.Player1)
             || x.Player2.Equals(player.Player2)))
                return false;

            Gamers.Add(player);

            return true;
        }

        private static void LocateShipsRandom(int count, int[,] battleTable)
        {
            for (int i = 0; i < count; i++)
            {
                int col, fila;
                Random rdn = new Random(unchecked((int)DateTime.Now.Ticks));
                col = rdn.Next(8);
                fila = rdn.Next(8);
                if (battleTable[col, fila] == 0)
                {
                    battleTable[col, fila] = 1;
                }
                else i--;
            }
        }
    }
}
