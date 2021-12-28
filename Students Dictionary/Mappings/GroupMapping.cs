using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Mappings
{
    public class GroupMapping : ClassMap<Model.Group>
    {
        public GroupMapping()
        {
            Table("Groups");
            Id(x => x.id).Column("ID").GeneratedBy.Assigned();
            Map(x => x.abbreviation).Column("ABBREVIATION").Not.Nullable();
            Map(x => x.course).Column("COURSE").Not.Nullable();
            HasMany(x => x.students).KeyColumn("GROUP_ID").Inverse().Cascade.All();
            References(x => x.speciality, "SPECIALITY_ID").Cascade.None();
        }
    }
}
