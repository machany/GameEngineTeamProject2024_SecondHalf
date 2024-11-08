using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Transform _mainCenter;
    [SerializeField] private LineGroup _lineGroup;

    private void Awake()
    {
        GameReset();
    }

    private void GameReset()
    {
        _lineGroup.GetLinePosType(LineType.Input, LineGroupType.Red).Add(_mainCenter);
        _lineGroup.GetLinePosType(LineType.Input, LineGroupType.Blue).Add(_mainCenter);
        _lineGroup.GetLinePosType(LineType.Input, LineGroupType.Green).Add(_mainCenter);
        _lineGroup.GetLinePosType(LineType.Output, LineGroupType.Red).Add(_mainCenter);
        _lineGroup.GetLinePosType(LineType.Output, LineGroupType.Blue).Add(_mainCenter);
        _lineGroup.GetLinePosType(LineType.Output, LineGroupType.Green).Add(_mainCenter);
    }
}
