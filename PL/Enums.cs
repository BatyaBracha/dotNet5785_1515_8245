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

internal class CallStatusCollection : IEnumerable
{
    static readonly IEnumerable<BO.CallStatus> s_enums =
                  (Enum.GetValues(typeof(BO.CallStatus)) as IEnumerable<BO.CallStatus>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}


internal class CallFieldsCollection : IEnumerable
{
    static readonly IEnumerable<BO.CallInListField> s_enums =
                  (Enum.GetValues(typeof(BO.CallInListField)) as IEnumerable<BO.CallInListField>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

public class TypeOfCallCollection : IEnumerable
{
    static readonly IEnumerable<BO.TypeOfCall> s_enums =
                  (Enum.GetValues(typeof(BO.TypeOfCall)) as IEnumerable<BO.TypeOfCall>)!;

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