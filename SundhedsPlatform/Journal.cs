using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundhedsPlatform
{
    class Journal
    {
        private string name;
        private string address;
        private string phone;
        private string email;
        private string cpr;
        private string doctor;
        private List<JournalEntry> entries;

        // Constructor
        public Journal(string[] details)
        {
            this.Name = details[0];
            this.Address = details[1];
            this.Phone = details[2];
            this.Email = details[3];
            this.Cpr = details[4];
            this.Doctor = details[5];
            this.entries = new List<JournalEntry>();
        }

        // Creating and adding journal entries
        public JournalEntry AddJournalEntry(string doctorInput, string descriptionInput, string dateOfCreation = null)
        {
            JournalEntry entry = new JournalEntry(doctorInput, descriptionInput, dateOfCreation);
            // Always puts the newer entries first in list
            this.entries.Insert(0, entry);
            return entry;
        }

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                this.address = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                this.phone = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
            }
        }
        public string Cpr
        {
            get
            {
                return cpr;
            }
            private set
            {
                this.cpr = value;
            }
        }
        public string Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                this.doctor = value;
            }
        }
        public List<JournalEntry> Entries
        {
            get
            {
                return entries;
            }
            set
            {
                this.entries = value;
            }
        }
        #endregion
    }
}
