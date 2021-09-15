using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundhedsPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            bool looper = true;
            Manager manager = new Manager();
            MainMenu();

            // The main menu screen
            #region Main Menu
            void MainMenu()
            {
                // Main Menu ->
                while (looper)
                {
                    TopMenuDesign();

                    Console.WriteLine("[ESC] - Exit");
                    Console.WriteLine("[1] -  Create new journal");
                    Console.WriteLine("[2] - Find existing journal");
                    Console.WriteLine();

                    ConsoleKey mainMenuChoice = Console.ReadKey().Key;

                    switch (mainMenuChoice)
                    {
                        case ConsoleKey.Escape:
                            looper = false;
                            break;
                        case ConsoleKey.D1:
                            CreateJournalMenu();
                            break;
                        case ConsoleKey.D2:
                            FindJournalMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            // Create Journal menu
            #region Create Journal Menu
            void CreateJournalMenu()
            {
                // Main Menu -> Create Journal ->
                TopMenuDesign();

                Console.WriteLine("Fill out journal infomation");

                // A string array to hold the basic information on the journal
                // we know this will be 6 lines
                string[] details = new string[6];

                Console.WriteLine("\nName: ");
                details[0] = Console.ReadLine(); // Name
                Console.WriteLine("\nAddress: ");
                details[1] = Console.ReadLine(); // Address
                Console.WriteLine("\nPhone: ");
                details[2] = Console.ReadLine(); // Phone
                Console.WriteLine("\nEmail: ");
                details[3] = Console.ReadLine(); // Email
                Console.WriteLine("\nCPR: ");
                details[4] = Console.ReadLine(); // CPR
                // Keeps asking the user for inputs until they use a valid CPR number
                while (manager.CprChecker(details[4]) == false)
                {
                    Console.WriteLine("\nCPR:\nMust be 10 characters all numbers and a accepted date: ");
                    details[4] = Console.ReadLine(); // CPR
                }
                Console.WriteLine("\nDoctor: ");
                details[5] = Console.ReadLine(); // Doctor
                Console.Clear();


                while (looper)
                {
                    // Displays the infomation the user has written
                    Console.WriteLine($"Name:\t{details[0]}");
                    Console.WriteLine($"Addr:\t{details[1]}");
                    Console.WriteLine($"Phone:\t{details[2]}");
                    Console.WriteLine($"Email:\t{details[3]}");
                    Console.WriteLine($"CPR:\t{details[4]}");
                    Console.WriteLine($"Doctor:\t{details[5]}");
                    Console.WriteLine();
                    Console.WriteLine("Is this the correct information?");
                    Console.WriteLine();
                    Console.WriteLine("[ESC] - Exit");
                    Console.WriteLine("[ENTER] - accept and create journal");
                    Console.WriteLine();

                    ConsoleKey journalMenuChoice = Console.ReadKey().Key;
                    switch (journalMenuChoice)
                    {
                        case ConsoleKey.Escape:
                            return;
                        case ConsoleKey.Enter:
                            // Check whether the file already exist,
                            // if so it will display the existing journal
                            bool fileExist;
                            Journal patient = manager.CreateJournal(details, out fileExist);
                            if (fileExist)
                            {
                                Console.WriteLine("File already exist. Opening existing file now");
                                Console.WriteLine("Press any key to continue");
                                Console.ReadKey();
                            }
                            DisplayJournalMenu(patient);
                            return;
                        default:
                            break;
                    }
                }

            }
            #endregion

            // Find journal Menu
            #region Find Journal Menu
            void FindJournalMenu()
            {
                // Main Menu -> Find Journal ->
                while (looper)
                {
                    TopMenuDesign();

                    Console.WriteLine("Write CPR of the persons journal you are looking for");

                    string cprInput = Console.ReadLine();

                    //Checks if the file exists, if not the user can try again or return to main menu
                    bool fileFound;
                    Journal loadedJournal = manager.LoadJournal(cprInput, out fileFound);
                    if (fileFound)
                    {
                        DisplayJournalMenu(loadedJournal);
                        // return makes sure that when the user presses ESC to exit the display menu
                        // they will arrive at the main menu, instead of being "stuck" in this one
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Journal does not exist.");
                        Console.WriteLine("[ESC] - Return to main menu");
                        Console.WriteLine("[ENTER] - Try again");
                        Console.WriteLine();

                        ConsoleKey journalMenuChoice = Console.ReadKey().Key;

                        switch (journalMenuChoice)
                        {
                            case ConsoleKey.Escape:
                                return;
                            case ConsoleKey.Enter:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion

            // Display Journal Menu
            #region Display Journal Menu
            void DisplayJournalMenu(Journal patient)
            {
                int currentEntry = 0;
                // Main Menu -> Create Journal/Find Journal -> Display Journal ->
                while (looper)
                {
                    TopMenuDesign();

                    Console.WriteLine($"Name:\t{patient.Name}\t\t\tCPR:\t{patient.Cpr}");
                    Console.WriteLine($"Addr.:\t{patient.Address}");
                    Console.WriteLine($"Phone:\t{patient.Phone}\t\t\tDoctor:\t{patient.Doctor}");
                    Console.WriteLine($"Email:\t{patient.Email}\t\t\t{manager.AgeToYD(patient.Cpr)}");
                    Console.WriteLine();
                    Console.WriteLine("==================================================================");
                    Console.WriteLine();
                    Console.WriteLine("[ESC] - Exit");
                    Console.WriteLine("[ENTER] - Create new journal entry");
                    Console.WriteLine("[ARROW KEYS] - Navigate entries");
                    Console.WriteLine();
                    // Displays the current selected entry or none if the journal has none
                    if (patient.Entries.Count == 0)
                    {
                        Console.WriteLine("No entries to display");
                    }
                    else
                    {
                        Console.WriteLine("{0}/{1}", currentEntry + 1, patient.Entries.Count);
                        string entryDate = patient.Entries[currentEntry].CreationDate.ToString("dd:MM:yyyy - HH:mm");
                        Console.WriteLine("==================================================================");
                        Console.WriteLine($"{entryDate} - {patient.Entries[currentEntry].Doctor}:");
                        Console.WriteLine(patient.Entries[currentEntry].Description.Replace("[newline]", "\n"));
                    }

                    ConsoleKey journalMenuChoice = Console.ReadKey().Key;

                    switch (journalMenuChoice)
                    {
                        case ConsoleKey.Escape:
                            return;
                        case ConsoleKey.Enter:
                            CreateEntryMenu(patient);
                            break;
                        case ConsoleKey.RightArrow:
                            // Goes to the next entry
                            currentEntry = manager.JournalNavigator(+1, currentEntry, patient.Entries.Count);
                            break;
                        case ConsoleKey.LeftArrow:
                            // Goes to the previous entry
                            currentEntry = manager.JournalNavigator(-1, currentEntry, patient.Entries.Count);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            // Create new entry menu
            #region Create Entry Menu
            void CreateEntryMenu(Journal patient)
            {
                // Main Menu -> Create Journal/Find Journal -> Display Journal -> Create Entyr ->
                while (looper)
                {
                    TopMenuDesign();

                    Console.WriteLine("Which doctor performed to examination?");
                    string doctorInput = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine("Write the description of the examination? (write end to stop)");
                    string descInput = "";
                    string input = "";
                    // Lets the user write multiple lines as the description
                    // Everytime the user makes a new line it will add [newline] to the string
                    // [newline] will then later be replaces with \n to write it out as actual 
                    // new lines when displayed. The user can type end to finish writing.
                    while ((input = Console.ReadLine()).ToLower() != "end")
                    {
                        descInput += input;
                        descInput += "[newline]";
                    }

                    TopMenuDesign();
                    
                    Console.WriteLine("Is this the entry you want to add?");
                    Console.WriteLine($"Doctor: {doctorInput}");
                    Console.WriteLine();
                    Console.WriteLine("Description:");
                    // Replaces the [newline] with \n to display the description correctly
                    string descFormatted = descInput.Replace("[newline]", "\n");
                    Console.WriteLine(descFormatted);

                    Console.WriteLine();
                    Console.WriteLine("[ESC] - discard and exit");
                    Console.WriteLine("[ENTER] - Create new journal entry");

                    ConsoleKey journalMenuChoice = Console.ReadKey().Key;
                    switch (journalMenuChoice)
                    {
                        case ConsoleKey.Escape:
                            return;
                        case ConsoleKey.Enter:
                            manager.CreateEntry(patient, doctorInput, descInput);
                            return;
                        default:
                            break;
                    }
                }
            }
            #endregion

            // Top Menu design
            #region Top Menu Design
            void TopMenuDesign()
            {
                // Used this at top of every menu
                Console.Clear();
                Console.WriteLine("==================================================================");
                Console.WriteLine("                    H1 Health Clinic Operations");
                Console.WriteLine("==================================================================");
                Console.WriteLine();
            }
            #endregion
        }

    }
}
