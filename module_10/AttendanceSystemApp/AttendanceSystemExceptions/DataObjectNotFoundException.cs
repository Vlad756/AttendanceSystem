using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceSystemApp.AttendanceSystemExceptions
{
    public class DataObjectNotFoundException : Exception
    {
        public DataObjectNotFoundException() { }
        public DataObjectNotFoundException(string message) : base(message) { }
    }
}
