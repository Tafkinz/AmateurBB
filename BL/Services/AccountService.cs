using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.DTO;
using BL.Factories;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Model;

namespace BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IContactsFactory _contactsFactory;
        private readonly IUserFactory _userFactory;

        public AccountService(IAppUnitOfWork uow, IContactsFactory contactsFactory, IUserFactory userFactory)
        {
            _uow = uow;
            _contactsFactory = contactsFactory;
            _userFactory = userFactory;
        }
        public ContactsDTO AddContact(ContactsDTO dto)
        {
            var contact = _contactsFactory.Create(dto);
            var result = _uow.Contacts.Add(contact);
            _uow.SaveChanges();
            return _contactsFactory.Create(result);
        }

        public ContactsDTO UpdateContact(long id, ContactsDTO dto)
        {
            var contact = _uow.Contacts.Find(id);
            if (contact == null) return null;

            contact.ContactValue = dto.Value;

            var result = _uow.Contacts.Update(contact);
            _uow.SaveChanges();
            return _contactsFactory.Create(result);
        }

        public ContactsDTO RemoveContactById(long contactId)
        {
            var contact = _uow.Contacts.Find(contactId);
            if (contact == null) return null;

            _uow.Contacts.RemoveById(contactId);
            _uow.SaveChanges();
            return _contactsFactory.Create(contact);
        }

        public ContactsDTO GetContactById(long contactId)
        {
            var contact = _uow.Contacts.Find(contactId);
            if (contact == null) return null;

            return _contactsFactory.Create(contact);
        }

        public List<ContactsDTO> GetAllContactsForUser(string userId)
        {
            var contacts = _uow.Contacts.GetAll().Where(p => p.ApplicationUser.Id == userId).ToList();
            List<ContactsDTO> contactsList = new List<ContactsDTO>();
            foreach (var contact in contacts)
            {
                contactsList.Add(_contactsFactory.Create(contact));
            }

            return contactsList;
        }

        public ContactTypeDTO AddContactType(ContactTypeDTO type)
        {
            if (_uow.GetCustomRepository<IContactTypeRepository>().Exists(type.ContactTypeName))
            {
                return type;
            }
            var result = _uow.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = type.ContactTypeName
            });
            _uow.SaveChanges();
            return _contactsFactory.Create(result);
        }

        public List<ContactTypeDTO> GetAllContactTypes()
        {
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
            var type = _uow.PersonTypes.Find(id);
            if (type == null) return null;

            return _userFactory.Create(type);
        }

        public PersonTypeDTO AddPersonType(PersonTypeDTO type)
        {
            if (_uow.GetCustomRepository<IPersonTypeRepository>().Exists(type.PersonTypeName))
            {
                return type;
            }
            var result = _uow.PersonTypes.Add(new PersonType()
            {
                PersonTypeName = type.PersonTypeName
            });
            _uow.SaveChanges();
            return _userFactory.Create(result);
        }

        public PersonTypeDTO UpdatePersonType(long id, PersonTypeDTO type)
        {
            var personType = _uow.PersonTypes.Find(id);
            if (personType == null) return null;
            if (_uow.GetCustomRepository<IPersonTypeRepository>().Exists(type.PersonTypeName))
            {
                return type;
            }
            personType.PersonTypeName = type.PersonTypeName;
            var result = _uow.PersonTypes.Update(personType);
            _uow.SaveChanges();
            return _userFactory.Create(result);
        }

        public ContactTypeDTO UpdateContactType(long id, ContactTypeDTO type)
        {
            var contactType = _uow.ContactTypes.Find(id);
            if (contactType == null) return null;
            contactType.ContactTypeName = type.ContactTypeName;
            var result = _uow.ContactTypes.Update(contactType);
            _uow.SaveChanges();
            return _contactsFactory.Create(result);
        }
    }
}
