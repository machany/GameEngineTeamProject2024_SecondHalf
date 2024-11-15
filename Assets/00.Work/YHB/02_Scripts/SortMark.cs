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

    public void ChangeRequestSortMark(int n = 1)
    {
        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                _sortRequestTargets.Add(Instantiate(CompanyInfo.Instance.RequestMark, transform));
                _sortRequestTargets[_sortRequestTargets.Count - 1].transform.position = transform.position;
            }
            // Ǯ�� Pop �ʿ� _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        else if (_sortRequestTargets.Count <= 0 || n == 0)
            return;
        else
        {
            for (int i = 0; i < n; i++)
            {
                _sortRequestTargets[_sortRequestTargets.Count - 1].Destroy();
                _sortRequestTargets.RemoveAt(_sortRequestTargets.Count - 1);
            }
            // Ǯ�� Push �ʿ� _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        SortRequest();
    }

    public void ChangeProductSortMark(int n = 1)
    {
        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                _sortProductTargets.Add(Instantiate(CompanyInfo.Instance.ProductMark, transform));
                _sortProductTargets[_sortProductTargets.Count - 1].transform.position = transform.position;
            }
            // Ǯ�� Pop �ʿ� _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        else if (_sortProductTargets.Count <= 0 || n == 0)
            return;
        else
        {
            for (int i = 0; i < n; i++)
            {
                _sortProductTargets[_sortProductTargets.Count - 1].Destroy();
                _sortProductTargets.RemoveAt(_sortProductTargets.Count - 1);
            }
            // Ǯ�� Push �ʿ� _sortRequestTargets[_sortRequestTargets.Count - 1]
        }
        SortProduct();
    }

    private void SortRequest()
    {
        float interval = CompanyInfo.Instance.interval * _sortRequestTargets.Count;

        if (_sortRequestTargets.Count <= 0)
            return;
        else if (_sortRequestTargets.Count == 1)
        {
            _sortRequestTargets[0].transform.DOMove(new Vector2(linear(interval, 0.5f, 1), CompanyInfo.Instance.requestPos) + (Vector2)transform.position, CompanyInfo.Instance.DuringTime);
            return;
        }

        float distance = 1f / (_sortRequestTargets.Count - 1);

        for (int i = 0; i < _sortRequestTargets.Count; i++)
            _sortRequestTargets[i].transform.DOMove(new Vector2(linear(interval, distance, i), CompanyInfo.Instance.requestPos) + (Vector2)transform.position, CompanyInfo.Instance.DuringTime);
    }

    private void SortProduct()
    {
        float interval = CompanyInfo.Instance.interval * _sortProductTargets.Count;

        if (_sortProductTargets.Count <= 0)
            return;
        else if (_sortProductTargets.Count == 1)
        {
            _sortProductTargets[0].transform.DOMove(new Vector2(linear(interval, 0.5f, 1), CompanyInfo.Instance.productPos) + (Vector2)transform.position, CompanyInfo.Instance.DuringTime);
            return;
        }

        float distance = 1f / (_sortProductTargets.Count - 1);

        for (int i = 0; i < _sortProductTargets.Count; i++)
            _sortProductTargets[i].transform.DOMove(new Vector2(linear(interval, distance, i), CompanyInfo.Instance.productPos) + (Vector2)transform.position, CompanyInfo.Instance.DuringTime);
    }

    // 0 ~ 2 * pi������ (0 ~ 1)������ ��ġ�� ����
    private float linear(float criteria, int number)
        => Mathf.Lerp(0, 2 * Mathf.PI, criteria * number);

    private float linear(float distance, float criteria, int number)
        => Mathf.Lerp(-distance / 2, distance / 2, criteria * number);
}
