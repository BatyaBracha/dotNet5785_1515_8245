using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;
internal class VolunteerFieldsCollection : IEnumerable
{
    static readonly IEnumerable<BO.VolunteerFields> s_enums =
                  (Enum.GetValues(typeof(BO.VolunteerFields)) as IEnumerable<BO.VolunteerFields>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

public class Role : IEnumerable
{
    static readonly IEnumerable<BO.Role> s_enums =
                  (Enum.GetValues(typeof(BO.Role)) as IEnumerable<BO.Role>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
public class TypeOfDistance : IEnumerable
{
    static readonly IEnumerable<BO.TypeOfDistance> s_enums =
                  (Enum.GetValues(typeof(BO.TypeOfDistance)) as IEnumerable<BO.TypeOfDistance>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

