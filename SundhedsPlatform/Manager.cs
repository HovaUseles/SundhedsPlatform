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
        public Journal CreateJournal(string[] details)
        {
            Journal patient = connector.JournalCreation(details);
            dal.CreateFile(patient);
            return patient;
        }

        public void CreateEntry(Journal patient, string doctor, string description, string dateOfCreation = null)
        {
            JournalEntry entry = patient.AddJournalEntry(doctor, description, dateOfCreation);
            dal.AddToFile(patient.Cpr, entry);
        }

        // Calls the method for importing a file 
        public Journal LoadJournal(uint cpr, out string ageYD, out bool fileExist)
        {
            string[,] entries;
            string[] patientDetails = dal.LoadFromFile(cpr, out entries);
            if (patientDetails != null)
            {
                // Using cpr number to calculate the age of the 
                ageYD = connector.AgeCalculator(cpr);
                // Tells the GUI that the file was found and returns a journal object
                fileExist = true;
                Journal patient = CreateJournal(patientDetails);

                // Creates journal entries from the file loaded
                for (int i = 0; i < entries.GetLength(0); i++)
                {
                    CreateEntry(patient, entries[i, 1], entries[i, 2], entries[i, 0]);
                }
                return patient;
            }
            else
            {
                ageYD = "";
                fileExist = false;
                return null;
            }
        }

        public JournalEntry JournalNavigator(sbyte key)
        {

        }
    }
}
