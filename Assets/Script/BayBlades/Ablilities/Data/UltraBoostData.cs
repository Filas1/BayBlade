using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Abilities/UltraBoostData")]
public class UltraBoostData : BaseRestrictedAbilityData
{
    [SerializeField] private float speedBoost = 20f;
    [CreateProperty] public float SpeedBoost => speedBoost;
}