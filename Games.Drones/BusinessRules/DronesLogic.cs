using Games.Drones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Drones.BusinessRules
{
    public class DronesLogic
    {
        public static int SetRoundWinner(Round round)
        {
            if (round.Player1MoveId == round.Player2MoveId)
            {
                return 0;
            }
            if (round.Player1MoveId == 1 && round.Player2MoveId == 2)
            {
                return 2;
            }
            if (round.Player1MoveId == 1 && round.Player2MoveId == 3)
            {
                return 1;
            }
            if (round.Player1MoveId == 2 && round.Player2MoveId == 3)
            {
                return 2;
            }
            if (round.Player1MoveId == 2 && round.Player2MoveId == 1)
            {
                return 1;
            }
            if (round.Player1MoveId == 3 && round.Player2MoveId == 2)
            {
                return 1;
            }
            if (round.Player1MoveId == 3 && round.Player2MoveId == 1)
            {
                return 2;
            }
            return 0;
        }

        public static int UpdateGameWinner(List<Round> rounds)
        {
            bool winner1 = rounds.Where(rd => rd.WinnerPlayer == 1).Count() >= 3;
            bool winner2 = rounds.Where(rd => rd.WinnerPlayer == 2).Count() >= 3;
            if (winner1)
            {
                return 1;
            }
            else if (winner2)
            {
                return 2;
            }
            return 0;
        }
    }
}