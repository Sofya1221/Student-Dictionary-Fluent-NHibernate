using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Model
{
    public class Speciality
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual int yearsOfStudy { get; set; }

        public virtual IList<Group> groups { get; set; }
    }
}
