using UnityEngine;

public abstract class BaseAbilityData : ScriptableObject
{
    [field:SerializeField] public float CooldownInSeconds{get;private set;}
}