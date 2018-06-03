using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using BL.Factories;
using DAL.App.Interfaces;
using Model;

namespace BL.Services
{
    public class CourtService : ICourtService
    {
        private readonly ICourtFactory _courtFactory;
        private readonly IAppUnitOfWork _uow;

        public CourtService(ICourtFactory courtFactory, IAppUnitOfWork uow)
        {
            _courtFactory = courtFactory;
            _uow = uow;
        }
        public CourtDTO AddCourt(CourtDTO dto)
        {
            Court court = _courtFactory.Create(dto);
            var result = _uow.Courts.Add(court);
            _uow.SaveChanges();
            return _courtFactory.Create(result);
        }

        public CourtDTO UpdateCourt(long courtId, CourtDTO dto)
        {
            Court court = _uow.Courts.Find(courtId);
            if (court == null) return null;

            court.Location = dto.Location;
            court.Capacity = dto.Capacity;
            court.CourtName = dto.Name;

            var result = _uow.Courts.Update(court);
            _uow.SaveChanges();
            return _courtFactory.Create(result);
        }

        public List<CourtDTO> GetAllCourts()
        {
            var courts = _uow.Courts.GetAll();
            List<CourtDTO> courtDtos = new List<CourtDTO>();
            foreach (var c in courts)
            {
                courtDtos.Add(_courtFactory.Create(c));
            }

            return courtDtos;
        }

        public CourtDTO GetCourtById(long courtId)
        {
            var court = _uow.Courts.Find(courtId);
            if (court == null) return null;
            return _courtFactory.Create(court);
        }
    }
}
