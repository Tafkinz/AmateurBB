using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using Model;

namespace BL.Services
{
    public interface IAccountService
    {
        ContactsDTO AddContact(ContactsDTO dto);

        ContactsDTO UpdateContact(long id, ContactsDTO dto);

        ContactsDTO RemoveContactById(long contactId);
        ContactsDTO GetContactById(long contactId);

        List<ContactsDTO> GetAllContactsForUser(string userId);

        ContactTypeDTO AddContactType(ContactTypeDTO type);

        List<ContactTypeDTO> GetAllContactTypes();

        List<PersonTypeDTO> GetAllPersonTypes();

        PersonTypeDTO GetPersonTypeById(long id);

        PersonTypeDTO AddPersonType(PersonTypeDTO type);

        PersonTypeDTO UpdatePersonType(long id, PersonTypeDTO type);

        ContactTypeDTO UpdateContactType(long id, ContactTypeDTO name);

        ApplicationUser FindByEmailAsync(string email);

        UserDTO GetCurrentUser();
    }
}
