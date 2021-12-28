using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDictionary.Mappings
{
    public class SpecialityMapping : ClassMap<Model.Speciality>
    {
        public SpecialityMapping()
        {
            Table("Speciality");
            Id(x => x.id).Column("ID").GeneratedBy.Assigned();
            Map(x => x.name).Column("NAME").Not.Nullable();
            Map(x => x.yearsOfStudy).Column("YEARS_OF_STUDY").Not.Nullable();
            HasMany(x => x.groups).KeyColumn("SPECIALITY_ID").Inverse().Cascade.All();
        }
    }
}
