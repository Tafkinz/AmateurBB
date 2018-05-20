using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace BL.Util
{
    public class ResultUtil
    {
        public static Boolean IsGameConfirmable(GameResult gameResult)
        {
            int result = 0;
            if (gameResult.AwayTeamConfirmed) result += 1;
            if (gameResult.HomeTeamConfirmed) result += 1;
            if (gameResult.RefereeConfirmed) result += 1;

            return result >= 2;
        }
    }
}
