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
        private bool JournalChecker(uint cpr)
        {
            // A method to check if a theres a file in the folder
            string[] files = Directory.GetFiles(filePath);
            if (files.Contains(cpr.ToString() + ".txt"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Creates the folder to store journals in, if not already 
        private void CreateFolder()
        {
            Directory.CreateDirectory(filePath);
        }
        public void AddToFile(uint cpr, JournalEntry entry)
        {
            // Adds created journal entries to the journal file
            writer = File.AppendText($"{filePath}\\{cpr}.txt");
            writer.WriteLine("{0}*{1}*{2}", entry.CreationDate, entry.Doctor, entry.Description);
            writer.Close();
        }

        public bool CreateFile(Journal medFile)
        {
            CreateFolder();
            if (JournalChecker(medFile.Cpr))
            {
                return false;
            }
            else
            {
                File.Create($"{filePath}\\{medFile.Cpr}.txt");
                writer = new StreamWriter($"{filePath}\\{medFile.Cpr}.txt");
                writer.WriteLine(medFile.Name);
                writer.WriteLine(medFile.Address);
                writer.WriteLine(medFile.Phone);
                writer.WriteLine(medFile.Email);
                writer.WriteLine(medFile.Cpr);
                writer.WriteLine(medFile.Doctor);
                writer.Close();
                return true;
            }
        }

        public string[] LoadFromFile(uint cpr, out string[,] entries)
        {
            if (JournalChecker(cpr))
            {
                // If file exists
                reader = new StreamReader($"{filePath}\\{cpr}.txt");
                int numberOfLines = File.ReadAllLines($"{filePath}\\{cpr}.txt").Length;
                string[] details = new string[5];
                entries = new string[numberOfLines-5, 3];

                for (int i = 0; i < numberOfLines; i++)
                {
                    if (i <= 5)
                    {
                        // Loads the first 5 lines as journal details
                        details[i] = reader.ReadLine();
                    }
                    else
                    {
                        // After the first 5 lines loads additional lines as jounal entries
                        // Splitting the lines into an multidimensional array to easier convert it to journal entry object
                        string line = reader.ReadLine();
                        string[] lineCollection = line.Split('*');
                        entries[i, 0] = lineCollection[0];
                        entries[i, 1] = lineCollection[1];
                        entries[i, 2] = lineCollection[2];
                    }
                }
                reader.Close();
                return details;
            }
            else
            {
                // no file exist
                entries = null;
                return null;
            }
        }
    }
}
