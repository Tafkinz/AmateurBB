using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;

namespace BL.Services
{
    public interface ICourtService
    {
        CourtDTO AddCourt(CourtDTO dto);

        CourtDTO UpdateCourt(long courtId, CourtDTO dto);

        List<CourtDTO> GetAllCourts();

        CourtDTO GetCourtById(long courtId);

    }
}
