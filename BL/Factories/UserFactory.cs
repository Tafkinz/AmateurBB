using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IContactsFactory _contactsFactory;

        public UserFactory( IContactsFactory contactsFactory)
        {
            _contactsFactory = contactsFactory;
        }
        public UserDTO Create(ApplicationUser user)
        {
            return new UserDTO()
            {
                Contacts = _contactsFactory.Create(user.Contacts),
                FirstName = user.FirstName,
                LastName = user.LastName,
                PersonType = user.PersonType,
                UserId = user.Id
            };
        }

        public ApplicationUser Create(UserDTO user)
        {
            return new ApplicationUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.UserId,
                PersonTypeId = user.PersonType.PersonTypeId
            };
        }
    }
}
