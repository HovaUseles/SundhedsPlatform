using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundhedsPlatform
{
    class JournalEntry
    {

        private string doctor;
        private string description;
        private DateTime creationDate;
        public JournalEntry(string doctor, string description, string dateOfCreation = null)
        {
            this.Doctor = doctor;
            this.Description = description;
            if (dateOfCreation == null)
            {
                this.creationDate = DateTime.Now;
            }
            else
            {
                this.creationDate = DateTime.Parse(dateOfCreation);
            }
        }

        #region Properties
        public string Doctor
        {
            get
            {
                return doctor;
            }
            private set
            {
                this.doctor = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                this.description = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
            private set
            {
                this.creationDate = value;
            }
        }
        #endregion
    }
}
