using JetBrains.Annotations;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "GameData/GlobalAbilityData")]
public class GlobalAbilityData : ScriptableObject
{
    [SerializeField] private Ability ability;
    [CreateProperty] Ability Ability => ability;
    /// <summary>
    /// If name is overriden then use it instead of using the class name
    /// </summary>
    [SerializeField, CanBeNull] private string abilityNameOverride;
    [CreateProperty] public string AbilityName => abilityNameOverride ?? ability.GetType().ToString();
    [SerializeField] private string abilityDescription;
    [SerializeField] private string abilityIcon;
}