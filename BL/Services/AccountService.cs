using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.Factories;
using BL.Util;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Model;

namespace BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IContactsFactory _contactsFactory;
        private readonly IUserFactory _userFactory;
        private readonly AuthUtil _auth;

        public AccountService(IAppUnitOfWork uow, IContactsFactory contactsFactory, IUserFactory userFactory, AuthUtil auth)
        {
            _uow = uow;
            _contactsFactory = contactsFactory;
            _userFactory = userFactory;
            _auth = auth;
        }
        public ContactsDTO AddContact(ContactsDTO dto)
        {
            //Add contact and return result
            var contact = _contactsFactory.Create(dto);
            var result = _uow.Contacts.Add(contact);
            result = _uow.GetCustomRepository<IContactRepository>().GetContact(result.ContactId);
            _uow.SaveChanges();
            return _contactsFactory.Create(result);
        }

        public ContactsDTO UpdateContact(long id, ContactsDTO dto)
        {
            //Find existing contact by id, if not found return null
            var contact = _uow.GetCustomRepository<IContactRepository>().GetContact(id);
            if (contact == null) return null;

            //Update selected contact and return it
            contact.ContactValue = dto.Value;
            contact.ContactTypeId = dto.ContactTypeId;

            _uow.Contacts.Update(contact);

            return _contactsFactory.Create(contact);
        }

        public ContactsDTO RemoveContactById(long contactId)
        {
            //Find contact to be removed and return null if not belonging to user or not found
            var contact = _uow.GetCustomRepository<IContactRepository>().GetContact(contactId);
            if (!_auth.IsCurrentUser(contact.ApplicationUserId))
            {
                return null;
            }
            if (contact == null) return null;

            //Remove it and return removed contact
            _uow.Contacts.RemoveById(contactId);
            _uow.SaveChanges();
            return _contactsFactory.Create(contact);
        }

        public ContactsDTO GetContactById(long contactId)
        {
            //Find and return, null if not found
            var contact = _uow.GetCustomRepository<IContactRepository>().GetContact(contactId);

            if (contact == null) return null;

            return _contactsFactory.Create(contact);
        }

        public List<ContactsDTO> GetAllContactsForUser(string userId)
        {
            //Get a list of contacts and iterate list to create DTO, then return
            var contacts = _uow.GetCustomRepository<IContactRepository>().GetByUserId(userId);
            List<ContactsDTO> contactsList = new List<ContactsDTO>();
            foreach (var contact in contacts)
            {
                contactsList.Add(_contactsFactory.Create(contact));
            }

            return contactsList;
        }

        public ContactTypeDTO AddContactType(ContactTypeDTO type)
        {
            //Find out if such type exists already and return it if does
            if (_uow.GetCustomRepository<IContactTypeRepository>().Exists(type.ContactTypeName))
            {
                return type;
            }
            //Otherwise create new
            var result = _uow.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = type.ContactTypeName
            });
            _uow.SaveChanges();
           
            return _contactsFactory.Create(result);
        }

        public List<ContactTypeDTO> GetAllContactTypes()
        {
            //Get all contact types, iterate list to create DTOs and return
            List<ContactType> types = _uow.ContactTypes.GetAll();
            List<ContactTypeDTO> result = new List<ContactTypeDTO>();
            foreach (ContactType type in types)
            {
                result.Add(_contactsFactory.Create(type));
            }
            return result;
        }

        public List<PersonTypeDTO> GetAllPersonTypes()
        {
            //Get all person types, iterate to DTOs and return
            List<PersonType> types = _uow.PersonTypes.GetAll();
            List<PersonTypeDTO> result = new List<PersonTypeDTO>();
            foreach (PersonType type in types)
            {
                result.Add(_userFactory.Create(type));
            }
            return result;
        }

        public PersonTypeDTO GetPersonTypeById(long id)
        {
            //Find type or return null if not found
            var type = _uow.PersonTypes.Find(id);
            if (type == null) return null;

            return _userFactory.Create(type);
        }

        public PersonTypeDTO AddPersonType(PersonTypeDTO type)
        {
            //Find if such type exists and return if does
            if (_uow.GetCustomRepository<IPersonTypeRepository>().Exists(type.PersonTypeName.ToString()))
            {
                return type;
            }
            //Otherwise create new
            var result = _uow.PersonTypes.Add(new PersonType()
            {
                PersonTypeName = type.PersonTypeName
            });
            _uow.SaveChanges();
            return _userFactory.Create(result);
        }

        public PersonTypeDTO UpdatePersonType(long id, PersonTypeDTO type)
        {
            //Find person type, if not found return null
            //If exists, return it
            var personType = _uow.PersonTypes.Find(id);
            if (personType == null) return null;
            if (_uow.GetCustomRepository<IPersonTypeRepository>().Exists(type.PersonTypeName.ToString()))
            {
                return type;
            }
            //Otherwise update and return
            personType.PersonTypeName = type.PersonTypeName;
            var result = _uow.PersonTypes.Update(personType);
            return _userFactory.Create(result);
        }

        public ContactTypeDTO UpdateContactType(long id, ContactTypeDTO type)
        {
            //Find contact type, null if not found
            var contactType = _uow.ContactTypes.Find(id);
            if (contactType == null) return null;
            //Update and return it
            contactType.ContactTypeName = type.ContactTypeName;
            var result = _uow.ContactTypes.Update(contactType);

            return _contactsFactory.Create(result);
        }

        public ApplicationUser FindByEmailAsync(string email)
        {
            //Find user by login
            return _uow.GetCustomRepository<IUserRepository>().FindByEmail(email);
        }

        public UserDTO GetCurrentUser()
        {
            //Find current user by token
            var user = _uow.GetCustomRepository<IUserRepository>().FindById(AuthUtil._userId);
            if (user == null) return null;

            user.Contacts = _uow.GetCustomRepository<IContactRepository>().GetByUserId(user.Id);
            return _userFactory.Create(user);
        }
    }
}
