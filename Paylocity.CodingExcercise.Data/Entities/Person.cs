using Paylocity.CodingExcercise.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingExcercise.Data.Entities
{
    public class Person
    {
        public string Name { get; set; }
        public PersonType Type { get; set; }
    }
}
