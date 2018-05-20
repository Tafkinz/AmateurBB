using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class StandingFactory : IStandingFactory
    {
        public StandingDTO Create(Standings s)
        {
            return new StandingDTO()
            {
                GamesPlayed = s.GamesPlayed,
                Losses = s.Losses,
                Points = s.Points,
                Wins = s.Wins,
                Team = s.Team.FullTeamName
            };
        }

        public Standings Create(StandingDTO s)
        {
            return new Standings()
            {
                GamesPlayed = s.GamesPlayed,
                Losses = s.Losses,
                Points = s.Points,
                Wins = s.Wins
            };
        }
    }
}

