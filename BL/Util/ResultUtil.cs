using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace BL.Util
{
    public class ResultUtil
    {
        public static Boolean IsGameConfirmable(Game gameResult)
        {
            int result = 0;
            if (gameResult.RefereeConfirmed) result += 1;
            if (gameResult.GameTeams[0].ResultConfirmed) result += 1;
            if (gameResult.GameTeams[1].ResultConfirmed) result += 1;

            return result >= 2;
        }
    }
}
