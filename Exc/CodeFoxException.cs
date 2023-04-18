using System;

namespace xelas_not_so_convenient_mysql_interface.Exc
{
    [Serializable]
    internal class CodeFoxException : Exception
    {
        /*
         * this is an error i defined that will be thrown when nothing can be done and the
         * program encountered a catastrophic error
         */
        public CodeFoxException()
        {

        }

        public CodeFoxException(string message) : base(message)
        {

        }

        public CodeFoxException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
