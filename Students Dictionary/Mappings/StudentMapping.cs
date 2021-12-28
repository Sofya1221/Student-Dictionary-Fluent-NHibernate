using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Mappings
{
    public class StudentMapping : ClassMap<Model.Student>
    {
        public StudentMapping()
        {
            Table("Students");
            Id(x => x.Id).Column("ID").GeneratedBy.Increment();
            Map(x => x.FirstName).Column("FIRSTNAME").Not.Nullable();
            Map(x => x.LastName).Column("LASTNAME").Not.Nullable();
            Map(x => x.Email).Column("EMAIL").Not.Nullable();
            References(x => x.Group, "GROUP_ID").Cascade.None();

        }
    }
}
