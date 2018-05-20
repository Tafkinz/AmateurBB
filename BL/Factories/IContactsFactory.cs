using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface IContactsFactory
    {
        ContactsDTO Create(Contact contact);
        Contact Create(ContactsDTO contactsDto);
        List<ContactsDTO> Create(List<Contact> contacts);
    }
}
