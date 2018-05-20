using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface ICourtFactory
    {
        CourtDTO Create(Court court);
        Court Create(CourtDTO courtDto);
    }
}
