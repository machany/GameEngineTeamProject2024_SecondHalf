using UnityEngine;

[CreateAssetMenu(menuName = "SO/Reward/RewardItemSO")]
public class RewardItemSO : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public ERewardItemType itemType;
}

public enum ERewardItemType
{
    Line,
    Bridge,
    Car,
    Truck,
    Trailer
}
