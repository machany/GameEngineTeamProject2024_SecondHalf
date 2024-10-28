using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;

public class LineGroup : MonoBehaviour
{
    public GameObject ir, ib, ig, or, ob, og;

    public GameObject GetLineGroupType(LineType lineType, LineGroupType lineGroupType) => lineGroupType switch
    {
        LineGroupType.Red when lineType == LineType.Input => ir,
        LineGroupType.Red => or,
        LineGroupType.Bule when lineType == LineType.Input => ib,
        LineGroupType.Bule => ob,
        LineGroupType.Green when lineType == LineType.Input => ig,
        LineGroupType.Green => og,
        _ => throw new Exception("wow, you seem like 57")
    };
}

public enum LineGroupType
{
    Red,
    Bule,
    Green
}

public enum LineType
{
    Input,
    Output
}


