using Unity.Properties;
using UnityEngine;

public abstract class BaseRestrictedAbilityData : BaseAbilityData
{
    [SerializeField, Range(1, 5)] private uint amountOfUses;
    [CreateProperty] public uint AmountOfUses => amountOfUses;
}