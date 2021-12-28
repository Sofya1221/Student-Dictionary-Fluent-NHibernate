using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Model
{
    public class Group
    {
        public virtual int id { get; set; }
        public virtual string abbreviation { get; set; }

        public virtual int course { get; set; }

        public virtual Speciality speciality { get; set; }

        public virtual IList<Student> students { get; set;}

        public override string ToString()
        {
            return abbreviation + " ("+speciality.name+")";
        }

        public override bool Equals(object obj)
        {
            return obj is Model.Group && ((Model.Group) obj).id == this.id;
        }
    }
}
