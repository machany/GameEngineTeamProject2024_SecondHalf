using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InputLine : MonoBehaviour
{
    public LineGroupType _lineGroupType;
    public LineType _lineType;
    
    private LineManager _lineManager;
    private LineGroup _lineGroup;
    
    private bool _isInput = false;
    private bool _isLine = false;
    public bool _isAlpha  = false;
    
    private GameObject _line;
    private GameObject _mainCenter;
    
    
    public Transform _startPos;
    public Transform _endPos;
    
    private void Awake()
    {
        _lineManager = GetComponent<LineManager>();
        _lineGroup = GetComponent<LineGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _lineGroupType = LineGroupType.Red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _lineGroupType = LineGroupType.Blue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _lineGroupType = LineGroupType.Green;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _lineType = LineType.Input;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _lineType = LineType.Output;
        }
        
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider)
                CheckHitCollider(hit);
            else
            {
                _line?.GetComponent<Line>().LineWidthReset();
                _isLine = false;
                _isAlpha = false;
                if (_line is not null)
                {
                    _lineManager.LineReSetAlpha(_line.GetComponent<Line>()._lineGroupType, _line.GetComponent<Line>()._lineType);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && _isLine) 
            _lineManager.RemoveList(_lineGroup.GetLineType(_line.GetComponent<Line>()._lineType, _line.GetComponent<Line>()._lineGroupType).IndexOf(_line),_line);
    }
    
    
    private void BuildingPos(Transform pos,bool isMain,bool isCenter)
    {
        if (!isMain && !isCenter)
        {
            if (_lineGroup.GetLinePosType(_lineType,_lineGroupType).Contains(pos)==_startPos)
            {
                return;
            }
        }
        
        if (!_isInput && !isCenter) 
        {
            _startPos = pos;
            _isInput = true; 
        }
        else 
        {
            if (_startPos != pos && !isMain) 
            {
                _endPos = pos;
                _isInput = false;
                if (_startPos != null && _endPos != null) 
                {
                    // 중복 연결 확인 후 라인 생성
                    if (!_lineManager.LineDuplicationTest(_startPos, _endPos)) 
                    {
                        _lineManager.CheckLineConnectionConditions(_startPos, _endPos);
                        _lineGroup.GetLinePosType(_lineType,_lineGroupType).Add(_endPos);
                        _startPos = null;
                        _endPos = null;
                    }
                }
            }
        }
    }
    
    //클릭한 오브젝트에 따라 처리 메쏘드
    private void CheckHitCollider(RaycastHit2D hit)
    {
        GameObject hitObject = hit.collider.gameObject;
        
        if (hitObject.GetComponent<Line>() is not null)
        {
            _line = hitObject;
            _line.GetComponent<Line>().LineWidthUp();
            if (!_isAlpha)
            {
                _lineManager.LineSetAlpha(_line.GetComponent<Line>()._lineGroupType, _line.GetComponent<Line>()._lineType);
                _isAlpha = true;
            }
            _isLine = true;
        }
        else if (hitObject.GetComponent<Buil>() is not null)
        {
            Buil builComponent = hitObject.GetComponent<Buil>();
            if (builComponent.Current < 2)
            {
                builComponent.Current++; 
                BuildingPos(hitObject.transform, false, false);
            }
            _line?.GetComponent<Line>().LineWidthReset();
            _isLine = false;
            if (_isAlpha)
            { 
                _lineManager.LineReSetAlpha(_line.GetComponent<Line>()._lineGroupType, _line.GetComponent<Line>()._lineType);
                _isAlpha = false;
            }
        }
        else if (hitObject.GetComponent<MainCenter>() is not null)
        {
            BuildingPos(hitObject.transform, true, false);
            _line?.GetComponent<Line>().LineWidthReset();
            _isLine = false;
            if (_isAlpha)
            {
                _lineManager.LineReSetAlpha(_line.GetComponent<Line>()._lineGroupType, _line.GetComponent<Line>()._lineType);
                _isAlpha = false;
            }
        }
        else if (hitObject.GetComponent<Center>() is not null)
        {
            BuildingPos(hitObject.transform, false, true);
            _line?.GetComponent<Line>().LineWidthReset();
            _isLine = false;
            if (_isAlpha)
            {
                _lineManager.LineReSetAlpha(_line.GetComponent<Line>()._lineGroupType, _line.GetComponent<Line>()._lineType);
                _isAlpha = false;
            }
        }
    }
}