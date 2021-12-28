using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Model
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }

        public virtual Group Group { get; set; }

        public override bool Equals(object obj)
        {
            return ((Model.Group) obj).id == this.Id;
        }
    }
}
