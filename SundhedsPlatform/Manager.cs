using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundhedsPlatform
{
    class Manager
    {
        JournalConnector connector = new JournalConnector();
        FileHandler dal = new FileHandler();
        public Manager()
        {
            //Default constructor
        }

        // Creates a new journal and returns an journal object to the GUI
        // Then stores the object in a file.
        #region Create new Journal
        public Journal CreateJournal(string[] details, out bool fileExist)
        {
            Journal patient = connector.JournalCreation(details);
            fileExist = dal.CreateFile(patient);

            // If file already exist loads that file instead
            if (fileExist)
            {
                patient = LoadJournal(details[4], out fileExist);
            }
            return patient;
        }
        #endregion

        // Creates a new entry and adds it to the correct file
        #region Create entry
        public void CreateEntry(Journal patient, string doctor, string description, string dateOfCreation = null)
        {
            // Creates a new journal entry
            JournalEntry entry = patient.AddJournalEntry(doctor, description, dateOfCreation);
            dal.AddToFile(patient.Cpr, entry);
        }
        #endregion

        // Method for importing a file 
        #region Load journal from file
        public Journal LoadJournal(string cpr, out bool fileExist)
        {
            string[,] entries;
            string[] patientDetails;
            dal.LoadFromFile(cpr, out patientDetails, out entries);
            if (patientDetails != null)
            {
                // Tells the GUI that the file was found and returns a journal object
                fileExist = true;
                Journal patient = connector.JournalCreation(patientDetails);

                // Creates journal entries from the file loaded
                for (int i = 0; i < entries.GetLength(0); i++)
                {
                    patient.AddJournalEntry(entries[i, 1], entries[i, 2], entries[i, 0]);
                }
                return patient;
            }
            else
            {
                fileExist = false;
                return null;
            }
        }
        #endregion

        // Converts CPR number to persons age in format of years + days
        #region Age Calculator
        public string AgeToYD(string cpr)
        {
            // Using cpr number to calculate the age of the 
            string birthDay;
            string birthMonth;
            string birthYear;
            connector.AgeCalculator(cpr, out birthDay, out birthMonth, out birthYear);
            string ageYD = connector.BirthdayToYD(birthDay, birthMonth, birthYear);

            return ageYD;
        }
        #endregion

        // Returns a number used to navigate through journal entries in a journal
        #region Journal Navigator
        public int JournalNavigator(sbyte key, int currentEntry, int entriesNumber)
        {
            currentEntry = connector.JournalNavigator(key, currentEntry, entriesNumber);
            return currentEntry;
        }
        #endregion

        // Checks if the cpr is only digits, correct length and within an actual date
        #region CPR Checker
        public bool CprChecker(string cpr)
        {
            bool isDigits = connector.CprChecker(cpr);
            return isDigits;
        }
        #endregion
    }
}
