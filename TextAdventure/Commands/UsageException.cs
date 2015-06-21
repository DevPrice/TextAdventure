using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Commands
{
    public class UsageException : Exception
    {
        public UsageException()
        {

        }

        public UsageException(string message)
            : base(message)
        {

        }
    }
}
