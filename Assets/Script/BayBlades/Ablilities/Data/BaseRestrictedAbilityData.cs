using Unity.Properties;
using UnityEngine;

public abstract class BaseRestrictedAbilityData : BaseAbilityData, IRestriced
{
    [SerializeField, Range(1, 5)] private uint amountOfUses;
    [CreateProperty] public uint AmountOfUses => amountOfUses;
}