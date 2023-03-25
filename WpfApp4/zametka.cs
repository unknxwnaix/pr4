using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp4
{
    class zametka
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sum { get; set; }
        public bool Isplus { get; set; }
        public string Date { get; set; }
        public zametka(string name, string type, string sum, bool isplus, string date) 
        {
            Name = name;
            Type = type;
            Sum = sum;
            Isplus = isplus;
            Date = date;
        }
    }
}
