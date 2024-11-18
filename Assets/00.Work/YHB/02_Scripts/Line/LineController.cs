using System;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Action OnLineChanged;

    [field : SerializeField] public LineType CurrentLineType {  get; private set; }
    [field: SerializeField] public LineGroupType CurrentGroup { get; private set; }

    private List<LineSO> lines = new List<LineSO>();
    private LineSO curLine;

    private void Update()
    {
        for (int i = 0; i < curLine.lineInfo.Count; i++)
        {
            Debug.Log(i + " : " + curLine.lineInfo[i].position);
        }

        SetLine(CurrentLineType, CurrentGroup);
    }

    public LineType GetLineType() => CurrentLineType;
    public LineGroupType GetGroupType() => CurrentGroup;

    private void Awake()
    {
        LineInitialize();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        LineMouseInput.Instance.OnClickCompany += HandleClickCompany;
    }

    private void LineInitialize()
    {
        foreach (LineType type in Enum.GetValues(typeof(LineType)))
            foreach (LineGroupType group in Enum.GetValues(typeof(LineGroupType)))
            {
                LineSO line = new LineSO();
                line.type = type;
                line.group = group;
                lines.Add(line);
            }

        curLine = lines[0];
    }

    private void HandleClickCompany(Transform companyTrm)
    {
        if (curLine.lineInfo.Contains(companyTrm))
        {
            curLine.lineInfo.Remove(companyTrm);
            return;
        }
        curLine.lineInfo.Add(companyTrm);
    }

    public void SetLine(LineType lineValue, LineGroupType groupValue)
    {
        CurrentLineType = lineValue;
        CurrentGroup = groupValue;

        lines.ForEach(lineInfo =>
        {
            if (lineInfo.type == lineValue && lineInfo.group == groupValue)
            {
                if (curLine == lineInfo)
                    return;

                curLine = lineInfo;
                OnLineChanged?.Invoke();
                return;
            }
        });
    }

    private void OnDestroy()
    {
        LineMouseInput.Instance.OnClickCompany -= HandleClickCompany;
    }
}
