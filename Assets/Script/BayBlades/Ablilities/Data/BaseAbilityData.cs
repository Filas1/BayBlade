using Unity.Properties;
using UnityEngine;

public abstract class BaseAbilityData : ScriptableObject
{
    [SerializeField] private float cooldownInSeconds = 20f;
    [CreateProperty] public float CooldownInSeconds => cooldownInSeconds;
}