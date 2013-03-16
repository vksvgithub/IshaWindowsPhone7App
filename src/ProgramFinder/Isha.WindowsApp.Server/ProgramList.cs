using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Isha.WindowsApp.Server
{
    public class ProgramList
    {
        public int resultcount;
        public List<Program> results { get; set; }

        public ProgramList()
        {
            results = new List<Program>();
        }
    }
}