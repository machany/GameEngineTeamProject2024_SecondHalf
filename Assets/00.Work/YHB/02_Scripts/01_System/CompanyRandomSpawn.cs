using System.Collections;
using UnityEngine;

public class CompanyRandomSpawn : MonoBehaviour, IInitialize
{
    [Header("Setting")]
    [SerializeField] private CompanyInfoSO companyInfo;
    [SerializeField] private PoolItemSO companyEffect;
    [Tooltip("게임 시작시 지정된 범위입니다.")]
    [SerializeField] private Vector2 firstRange = Vector2.zero;
    [Tooltip("게임 시작후 몇초 후에 회사를 생성할지 정합니다.")]
    [SerializeField] private float firstDelay = 10f;
    [Tooltip("몇번 동안 회사를 firstDelay로 소환할지 정합니다.")]
    [SerializeField] private int startDelayCount = 1;
    [Tooltip("firstDelay를 끝내고 시작할 Delay입니다.")]
    [SerializeField] private float startDelay = 10f;
    [Tooltip("회사 생성 후 추가할 Delay입니다.")]
    [SerializeField] private float addDelay = 5f;
    [Range(1f, 2f)]
    [Tooltip("addDelay를 더하고 나서 곱합니다.")]
    [SerializeField] private float multiplyDelay = 1.1f;
    [Tooltip("곱하기 시작하는 회사 생성 수 입니다.")]
    [SerializeField] private int multiplyStartCount = 5;
    [Tooltip("최대 생성 시간입니다.")]
    [SerializeField] private float maxDelayTime = 60f;

    [Header("Company")]
    [SerializeField] private PoolItemSO companySO;
    [SerializeField] private int maxCompanyCount = 15;

    [Header("Obstacle")]
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private float obstractRadius = 1f;

    [Header("Overlap")]
    [SerializeField] private LayerMask notOverlapObjectLayer;
    [SerializeField] private float overlapRadius = 1f;

    [Header("Building")]
    [SerializeField] private Transform building;
    private Vector2 _spawnRange = Vector2.zero;

    private int _count;
    private float _delayTime;
    private Vector2 _curSpawnRange;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _spawnRange = CameraControl.Instance.maxWorldBounds;
        _spawnRange.x -= obstractRadius;
        _spawnRange.y -= obstractRadius;

        overlapRadius += obstractRadius;

        // firstRange의 각 값을 overlapRadius * startDelayCount  1.5f보단 크고, _spawnRange.x보단 작음
        firstRange = new Vector2(Mathf.Clamp(firstRange.x, overlapRadius * startDelayCount / 1.5f, _spawnRange.x), Mathf.Clamp(firstRange.y, overlapRadius * startDelayCount / 1.5f, _spawnRange.y));

        _delayTime = 0;
        _count = 0;

        if (building is null)
        {
            building = new GameObject().transform;
            building.name = "Building";
        }

        SoundManager.Instance.PlaySound(SoundType.SFX, "GameStart");
        SoundManager.Instance.PlaySoundLoopInChannel(SoundType.BGM);
        StartCoroutine(SpawnProcces());
    }

    private IEnumerator SpawnProcces()
    {
        Vector2 addRange = new Vector2(Mathf.Lerp(0, _spawnRange.x - firstRange.x, 2f / maxCompanyCount), Mathf.Lerp(0, _spawnRange.y - firstRange.y, 2f / maxCompanyCount));
        CameraControl.Instance.curWorldBounds = firstRange * 2;


        for (int i = 0; i < startDelayCount; i++)
        {
            yield return new WaitForSeconds(firstDelay);
            SpawnCompany(firstRange);
        }

        _delayTime = startDelay;
        _curSpawnRange = firstRange;

        do
        {
            yield return new WaitForSeconds(_delayTime);

            companyInfo.productTime += companyInfo.productAddTime;
            companyInfo.requestTime += companyInfo.requestAddTime;

            _curSpawnRange = new Vector2(Mathf.Clamp(_curSpawnRange.x + addRange.x, firstRange.x, _spawnRange.x), Mathf.Clamp(_curSpawnRange.y + addRange.y, firstRange.y, _spawnRange.y));
            CameraControl.Instance.curWorldBounds = new Vector2(Mathf.Clamp(_curSpawnRange.x * 2, firstRange.x * 2, CameraControl.Instance.maxWorldBounds.x), Mathf.Clamp(_curSpawnRange.y * 2, firstRange.y * 2, CameraControl.Instance.maxWorldBounds.y));
        } while (SpawnCompany(_curSpawnRange));
    }

    private bool SpawnCompany(Vector3 spawnRange)
    {
        Vector3 targetPos;

        do
        {
            targetPos = new Vector2(Random.Range(-spawnRange.x / 2, spawnRange.x / 2), Random.Range(-spawnRange.y / 2, spawnRange.y / 2));
            // 회사 중복 불가 범위 체크에 걸리거나 방해물 체크에 걸리면 리셋
        } while (Physics2D.OverlapCircle(targetPos, overlapRadius, notOverlapObjectLayer) || Physics2D.OverlapCircle(targetPos, obstractRadius, obstacle));

        Company company = PoolManager.Instance.Pop(companySO.key).GetComponent<Company>();
        company.transform.position = targetPos;
        company.transform.parent = building;

        SoundManager.Instance.PlaySound(SoundType.SFX, "CompanyCreation");

        _delayTime = Mathf.Clamp((_delayTime + addDelay) * (_count <= multiplyStartCount ? multiplyDelay : 1), startDelayCount, maxDelayTime);

        ParticleSystem particle = PoolManager.Instance.Pop(companyEffect.key).GetComponent<ParticleSystem>();
        particle.transform.position = company.transform.position;
        particle.startColor = companyInfo.GetResourceColor(company.requestType);

        return !(_count++ >= maxCompanyCount);
    }

    public void Disable()
    {
        StopCoroutine(SpawnProcces());
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        float s = obstractRadius + overlapRadius;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, s);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, obstractRadius);
        Gizmos.color = Color.white;
    }

#endif
}
