using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHierarchy.Libs;

public class SortMark : MonoBehaviour, IInitialize
{
    private List<GameObject> _sortRequestTargets = new List<GameObject>();
    private List<GameObject> _sortProductTargets = new List<GameObject>();

    [SerializeField] private PoolItemSO ProductMark;
    [SerializeField] private PoolItemSO RequestMark;

    private int _beforeRequestCount, _beforeProductCount;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Disable();
    }

    public void Initialize()
    {
        transform.GetComponentInParent<Company>().OnRequestCostChanged += ChangeRequestSortMark;
        transform.GetComponentInParent<Company>().OnProductCostChanged += ChangeProductSortMark;
    }

    public void Disable()
    {
        transform.GetComponentInParent<Company>().OnRequestCostChanged -= ChangeRequestSortMark;
        transform.GetComponentInParent<Company>().OnProductCostChanged -= ChangeProductSortMark;
    }

    public void ChangeRequestSortMark(int n = 1)
    {
        if (n > _beforeRequestCount)
        {
            int save = n - _sortRequestTargets.Count;
            for (int i = 0; i < save; i++)
            {
                _sortRequestTargets.Add(PoolManager.Instance.Pop(RequestMark));
                _sortRequestTargets[_sortRequestTargets.Count - 1].transform.position = transform.position;
            }
        }
        else if (_beforeRequestCount.Equals(n))
            return;
        else
        {
            int save = _sortRequestTargets.Count - n;
            for (int i = 0; i < save; i++)
            {
                StartCoroutine(DeleteSortMark(RequestMark, _sortRequestTargets[_sortRequestTargets.Count - 1]));
                _sortRequestTargets.RemoveAt(_sortRequestTargets.Count - 1);
            }
        }
        _beforeRequestCount = n;
        SortRequest();
    }

    public void ChangeProductSortMark(int n = 1)
    {
        if (n > _beforeProductCount)
        {
            int save = n - _sortProductTargets.Count;
            for (int i = 0; i < save; i++)
            {
                _sortProductTargets.Add(PoolManager.Instance.Pop(ProductMark));
                _sortProductTargets[_sortProductTargets.Count - 1].transform.position = transform.position;
            }
        }
        else if (_beforeProductCount.Equals(n))
            return;
        else
        {
            int save = _sortProductTargets.Count - n;
            for (int i = 0; i < save; i++)
            {
                StartCoroutine(DeleteSortMark(ProductMark, _sortProductTargets[_sortProductTargets.Count - 1]));
                _sortProductTargets.RemoveAt(_sortProductTargets.Count - 1);
            }
        }
        _beforeProductCount = n;
        SortProduct();
    }

    private IEnumerator DeleteSortMark(PoolItemSO poolItem, GameObject gameObject)
    {
        gameObject.transform.DOMove(transform.position, CompanyManager.Instance.companyInfo.DuringTime);
        yield return new WaitForSeconds(CompanyManager.Instance.companyInfo.DuringTime);
        PoolManager.Instance.Push(poolItem, gameObject);
    }

    private void SortRequest()
    {
        float interval = CompanyManager.Instance.companyInfo.interval * _sortRequestTargets.Count;

        if (_sortRequestTargets.Count <= 0)
            return;
        else if (_sortRequestTargets.Count == 1)
        {
            _sortRequestTargets[0].transform.DOMove(new Vector2(linear(interval, 0.5f, 1), CompanyManager.Instance.companyInfo.requestPos) + (Vector2)transform.position, CompanyManager.Instance.companyInfo.DuringTime);
            return;
        }

        float distance = 1f / (_sortRequestTargets.Count - 1);

        for (int i = 0; i < _sortRequestTargets.Count; i++)
            _sortRequestTargets[i].transform.DOMove(new Vector2(linear(interval, distance, i), CompanyManager.Instance.companyInfo.requestPos) + (Vector2)transform.position, CompanyManager.Instance.companyInfo.DuringTime);
    }

    private void SortProduct()
    {
        float interval = CompanyManager.Instance.companyInfo.interval * _sortProductTargets.Count;

        if (_sortProductTargets.Count <= 0)
            return;
        else if (_sortProductTargets.Count == 1)
        {
            _sortProductTargets[0].transform.DOMove(new Vector2(linear(interval, 0.5f, 1), CompanyManager.Instance.companyInfo.productPos) + (Vector2)transform.position, CompanyManager.Instance.companyInfo.DuringTime);
            return;
        }

        float distance = 1f / (_sortProductTargets.Count - 1);

        for (int i = 0; i < _sortProductTargets.Count; i++)
            _sortProductTargets[i].transform.DOMove(new Vector2(linear(interval, distance, i), CompanyManager.Instance.companyInfo.productPos) + (Vector2)transform.position, CompanyManager.Instance.companyInfo.DuringTime);
    }

    // 0 ~ 2 * pi사이의 (0 ~ 1)비율의 위치를 구함
    private float linear(float criteria, int number)
        => Mathf.Lerp(0, 2 * Mathf.PI, criteria * number);

    private float linear(float distance, float criteria, int number)
        => Mathf.Lerp(-distance / 2, distance / 2, criteria * number);
}
