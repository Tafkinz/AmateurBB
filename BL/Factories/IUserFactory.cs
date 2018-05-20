using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface IUserFactory
    {
        UserDTO Create(ApplicationUser user);
        ApplicationUser Create(UserDTO user);
    }
}
