using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Util
{
    public class AuthUtil
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppUnitOfWork _uow;

        [ThreadStatic]
        public static string _userId;

        public AuthUtil(UserManager<ApplicationUser> userManager, IAppUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }
        public string GetCurrentUserId()
        {
            return _userId;
        }

        public static void SetUser(string userId)
        {
            _userId = userId;
        }

        public bool IsCurrentUser(string userId)
        {
            return GetCurrentUserId().Equals(_userId);
        }

        public bool IsAdmin()
        {
            ApplicationUser user = _uow.GetCustomRepository<IUserRepository>().FindById(_userId);

            return user.PersonType.PersonTypeName == Model.Enum.PersonTypes.Admin;
        }

        public bool IsManager()
        {
            string userId = GetCurrentUserId();
            ApplicationUser user = _uow.GetCustomRepository<IUserRepository>().FindById(_userId);

            return user.PersonType.PersonTypeName == Model.Enum.PersonTypes.Manager;
        }

    }
}
