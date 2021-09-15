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

        // Constructor
        public JournalEntry(string doctor, string description, string dateOfCreation = null)
        {
            this.Doctor = doctor;
            this.Description = description;
            // if its a new entry it will take the current time
            // If its a loaded file it will parse the line from the file into time format
            if (dateOfCreation == null)
            {
                // New entry, using the current time and date
                this.creationDate = DateTime.Now;
            }
            else
            {
                // If the file is loaded, the date is split up and is stoed in a new DateTime object
                string[] dateSplit = dateOfCreation.Split(':');
                int[] dateInfo = new int[dateSplit.Length];
                for (int i = 0; i < dateSplit.Length; i++)
                {
                    dateInfo[i] = Convert.ToInt32(dateSplit[i]);
                }
                this.creationDate = new DateTime(dateInfo[0], dateInfo[1], dateInfo[2], dateInfo[3], dateInfo[4], 0);
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
