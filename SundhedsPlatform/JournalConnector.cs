using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundhedsPlatform
{
    class JournalConnector
    {
        
        public JournalConnector()
        {
            //Default constructor
        }

        public string AgeCalculator(uint cpr)
        {

        }

        public JournalEntry JournalNavigator(sbyte key)
        {
            
        }

        public Journal JournalCreation(string[] details)
        {
            Journal patient = new Journal(details);
            return patient;
        }
    }
}
