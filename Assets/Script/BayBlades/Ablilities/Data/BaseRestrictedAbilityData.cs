using UnityEngine;

public abstract class BaseRestrictedAbilityData : BaseAbilityData
{
    [field:SerializeField, Range(1,5)] public uint AmountOfUses{get;private set;}
}