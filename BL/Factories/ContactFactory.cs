using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class ContactFactory : IContactsFactory
    {
        public ContactsDTO Create(Contact contact)
        {
            return new ContactsDTO()
            {
                ContactType = contact.ContactType.ContactTypeName,
                ContactTypeId = contact.ContactTypeId,
                Value = contact.ContactValue,
                ContactId = contact.ContactId
            };
        }

        public Contact Create(ContactsDTO contactsDto)
        {
            return new Contact()
            {
                ApplicationUserId = contactsDto.UserId,
                ContactTypeId = contactsDto.ContactTypeId,
                ContactValue = contactsDto.Value
            };
        }

        public List<ContactsDTO> Create(List<Contact> contacts)
        {
            List<ContactsDTO> contactsDto = new List<ContactsDTO>();
            foreach (Contact contact in contacts)
            {
                contactsDto.Add(Create(contact));
            }

            return contactsDto;
        }
    }
}
