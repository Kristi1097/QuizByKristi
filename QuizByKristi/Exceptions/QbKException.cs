using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizByKristi.Exceptions
{
    class QbKException : Exception
    {
        public QbKException()
        {
        }

        public QbKException(string message) : base(message)
        {
        }
    }
}
