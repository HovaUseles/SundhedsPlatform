using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace SundhedsPlatform
{
    class JournalConnector
    {

        public JournalConnector()
        {
            //Default constructor
        }

        // Compares a birthdate to today to return persons age in years & days
        #region Birthday to years & days
        public string BirthdayToYD(string birthDay, string birthMonth, string birthYear)
        {
            // using nodatime library to calculate the time since birth
            // First creates a date object from the birthDate numbers
            LocalDate dateOfBirth = new LocalDate(Convert.ToInt32(birthYear), Convert.ToInt32(birthMonth), Convert.ToInt32(birthDay));

            // Creates a object of todays date
            DateTime today = DateTime.Now.Date;
            LocalDate curruntTime = LocalDate.FromDateTime(today);

            // Compares the two objects and specify the units to years and days
            // Calculate age in years and days
            Period lifeTime = Period.Between(dateOfBirth, curruntTime, PeriodUnits.Years | PeriodUnits.Days);
            string ageYD = string.Format("{0} years, {1} days", lifeTime.Years, lifeTime.Days);

            return ageYD;
        }
        #endregion

        // Calculates a birthdate based on the CPR number
        // uses the 7 digit to control whether the person is over or under 100 years old
        #region Age Calculator
        public void AgeCalculator(string cpr, out string birthDay, out string birthMonth, out string birthYear)
        {
            string birthDecade = cpr[4].ToString() + cpr[5].ToString();
            int controlNumber = Convert.ToInt32(cpr[6].ToString());
            string century = "";
            // Using switch and ifs to find the right century to add to birthday
            switch (controlNumber)
            {
                // If control number is between 0 and 3
                case int n when n < 4:
                    // 1900-1999
                    century = "19";
                    break;
                // if control number is between 5 and 8
                case int n when (n > 3 && n < 9):
                    if (Convert.ToInt32(birthDecade) <= 57)
                    {
                        // 2000-2057
                        century = "20";
                    }
                    else
                    {
                        // 1858-1899
                        century = "18";
                    }
                    break;
                // If control number is 4 or 9
                case 4:
                case 9:
                    if (Convert.ToInt32(birthDecade) <= 36)
                    {
                        // 2000-2036
                        century = "20";
                    }
                    else
                    {
                        // 1937-1999
                        century = "19";
                    }
                    break;
            }

            // Combines the century and decade numbers to create the year of birth
            // also outputs the day and month of the birthdate
            birthYear = (century + birthDecade).ToString();
            birthDay = cpr[0].ToString() + cpr[1].ToString();
            birthMonth = cpr[2].ToString() + cpr[3].ToString();
        }
        #endregion

        // Checks if its the beginning or end of the entries list
        // and returns the next or previous index depending on which arrow key was pressed
        #region Journal navigator
        public int JournalNavigator(sbyte key, int currentEntry, int entriesNumber)
        {
            currentEntry = currentEntry + key;
            if (currentEntry < 0)
            {
                currentEntry = entriesNumber - 1;
            }
            else if (currentEntry > entriesNumber - 1)
            {
                currentEntry = 0;
            }
            return currentEntry;
        }
        #endregion

        // Creates a new journal object
        #region Journal Creation
        public Journal JournalCreation(string[] details)
        {
            Journal patient = new Journal(details);
            return patient;
        }
        #endregion

        // Goes through the CPR input and checks whether or not is consist of only digits
        // Also checks if the CPR is the correct length and a valid date
        #region Only digits check
        public bool CprChecker(string cpr)
        {
            // Checks if the CPR is the correct length
            if (cpr.Length != 10) return false;

            // Using the CPR to birthdate calculator we check if its a valid date
            string birthDay;
            string birthMonth;
            string birthYear;
            AgeCalculator(cpr, out birthDay, out birthMonth, out birthYear);
            string cprDate = string.Concat(birthDay, "-", birthMonth, "-", birthYear);
            DateTime someDate = new DateTime();
            if (!DateTime.TryParse(cprDate, out someDate)) return false;

            // Checks if the CPR is a series of number
            foreach (char character in cpr)
            {
                if (character < '0' || character > '9')
                    return false;
            }

            // If all the statements above are false the CPR is accepted
            return true;
        }
        #endregion
    }
}
