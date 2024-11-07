using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VHierarchy.Libs;

public class SortMark : MonoBehaviour
{
    private List<GameObject> _sortRequestTargets = new List<GameObject>();
    private List<GameObject> _sortProductTargets = new List<GameObject>();

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Disable();
    }

    private void Initialize()
    {
        transform.GetComponentInParent<Company>().OnRequestCostChanged += ChangeRequestSortMark;
        transform.GetComponentInParent<Company>().OnProductCostChanged += ChangeProductSortMark;
    }

    private void Disable()
    {
        transform.GetComponentInParent<Company>().OnRequestCostChanged -= ChangeRequestSortMark;
        transform.GetComponentInParent<Company>().OnProductCostChanged -= ChangeProductSortMark;
    }

    public void ChangeRequestSortMark(bool plus = true)
    {
        if (plus)
        {
            _sortRequestTargets.Add(Instantiate(CompanyInfo.Instance.RequestMark, transform));
            _sortRequestTargets[_sortRequestTargets.Count - 1].transform.position = transform.position;
            // 풀링 Pop 필요 _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        else if (_sortRequestTargets.Count <= 0)
            return;
        else
        {
            _sortRequestTargets[_sortRequestTargets.Count - 1].Destroy();
            _sortRequestTargets.RemoveAt(_sortRequestTargets.Count - 1);
            // 풀링 Push 필요 _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        SortRequest();
    }

    public void ChangeProductSortMark(bool plus = true)
    {
        if (plus)
        {
            _sortProductTargets.Add(Instantiate(CompanyInfo.Instance.ProductMark, transform));
            _sortProductTargets[_sortProductTargets.Count - 1].transform.position = transform.position;
            // 풀링 Pop 필요 _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        else if (_sortProductTargets.Count <= 0)
            return;
        else
        {
            _sortProductTargets[_sortProductTargets.Count - 1].Destroy();
            _sortProductTargets.RemoveAt(_sortProductTargets.Count - 1);
            // 풀링 Push 필요 _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        SortProduct();
    }

    private void SortRequest()
    {
        if (_sortRequestTargets.Count <= 0)
            return;

        float distance = 1f / _sortRequestTargets.Count;

        for (int i = 0; i < _sortRequestTargets.Count; i++)
            _sortRequestTargets[i].transform.DOMove(new Vector2(Mathf.Cos(linear(distance, i)), Mathf.Sin(linear(distance, i))) * CompanyInfo.Instance.RequestRadius
                + (Vector2)transform.position, CompanyInfo.Instance.DuringTime);
    }

    private void SortProduct()
    {
        if (_sortProductTargets.Count <= 0)
            return;

        float distance = 1f / _sortProductTargets.Count;

        for (int i = 0; i < _sortProductTargets.Count; i++)
            _sortProductTargets[i].transform.DOMove(new Vector2(Mathf.Cos(linear(distance, i)), Mathf.Sin(linear(distance, i))) * CompanyInfo.Instance.ProductRadius
                + (Vector2)transform.position, CompanyInfo.Instance.DuringTime);
    }

    // 0 ~ 2 * pi사이의 (0 ~ 1)비율의 위치를 구함
    private float linear(float criteria, int number)
        => Mathf.Lerp(0, 2 * Mathf.PI, criteria * number);
}
