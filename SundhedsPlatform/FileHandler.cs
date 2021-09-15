using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SundhedsPlatform
{
    class FileHandler
    {
        private string filePath = @".\HealthClinic";
        private StreamReader reader;
        private StreamWriter writer;

        public FileHandler()
        {
            //Default constructor
        }

        // Checks of a file exist in the folder
        #region File existance checker
        private bool JournalChecker(string cpr)
        {
            // A method to check if a theres a file in the folder
            string[] files = Directory.GetFiles(filePath);
            if (files.Contains(filePath + "\\" + cpr.ToString() + ".txt"))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion

        // Creates the folder to store journals in, if not already created 
        #region Create folder
        private void CreateFolder()
        {
            Directory.CreateDirectory(filePath);
        }
        #endregion

        // Saves entries to a file to be,loaded back later
        #region Add entry to file
        public void AddToFile(string cpr, JournalEntry entry)
        {
            // Adds created journal entries to the journal file
            writer = File.AppendText($"{filePath}\\{cpr}.txt");
            // Writes the entry information to one line, to be split up later on when loading in 
            writer.WriteLine("{0}*{1}*{2}", entry.CreationDate.ToString("yyyy:MM:dd:HH:mm"), entry.Doctor, entry.Description);
            writer.Close();
        } 
        #endregion

        // Creates a new file and writes the journal contents to it
        #region Create Journal File
        public bool CreateFile(Journal patient)
        {
            // Writes a new file 
            CreateFolder();
            if (JournalChecker(patient.Cpr))
            {
                // If file exist do nothing and tell the manager
                return true;
            }
            else
            {
                // If file does not exist create a new one
                writer = new StreamWriter($"{filePath}\\{patient.Cpr}.txt");
                writer.WriteLine(patient.Name);
                writer.WriteLine(patient.Address);
                writer.WriteLine(patient.Phone);
                writer.WriteLine(patient.Email);
                writer.WriteLine(patient.Cpr);
                writer.WriteLine(patient.Doctor);
                writer.Close();
                return false;
            }
        } 
        #endregion

        // Loads a text document and recreates it as an object to be manipulated
        #region Load Journal from file
        public void LoadFromFile(string cpr, out string[] details, out string[,] entries)
        {
            if (JournalChecker(cpr))
            {
                // If file exists
                reader = new StreamReader($"{filePath}\\{cpr}.txt");
                int numberOfLines = File.ReadAllLines($"{filePath}\\{cpr}.txt").Length;
                details = new string[6];
                entries = new string[numberOfLines - 6, 3];

                // Splits the file up in 2 arrays
                // One to hold the basic information about the journal
                // Another to hold the entries to recreate them seperately
                for (int i = 0; i < numberOfLines; i++)
                {
                    if (i <= 5)
                    {
                        // Loads the first 6 lines as journal details
                        details[i] = reader.ReadLine();
                    }
                    else
                    {
                        // After the first 6 lines loads additional lines as jounal entries
                        // Splitting the lines into an multidimensional array to easier convert it to journal entry object
                        string line = reader.ReadLine();
                        string[] lineCollection = line.Split('*');
                        entries[i - 6, 0] = lineCollection[0];
                        entries[i - 6, 1] = lineCollection[1];
                        entries[i - 6, 2] = lineCollection[2];
                    }
                }
                reader.Close();
            }
            else
            {
                // no file exist
                entries = null;
                details = null;
            }
        }
        #endregion
    }
}
