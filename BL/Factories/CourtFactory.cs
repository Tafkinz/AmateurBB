using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class CourtFactory : ICourtFactory
    {
        public CourtDTO Create(Court court)
        {
            return new CourtDTO()
            {
                Capacity = court.Capacity,
                CourtId = court.CourtId,
                Location = court.Location,
                Name = court.CourtName
            };
        }

        public Court Create(CourtDTO courtDto)
        {
            return new Court()
            {
                Capacity = courtDto.Capacity,
                CourtId = courtDto.CourtId,
                CourtName = courtDto.Name,
                Location = courtDto.Location
            };
        }
    }
}
