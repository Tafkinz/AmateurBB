using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface IStandingFactory
    {
        StandingDTO Create(Standings s);
        Standings Create(StandingDTO s);
    }
}
