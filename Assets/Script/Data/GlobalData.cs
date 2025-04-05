using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalData : ScriptableObject, ISingleton
{
    [Header("Available bayblades")]
    [SerializeField] private GlobalBaybladeData[] bayblades;
    [CreateProperty] public GlobalBaybladeData[] Bayblades => bayblades;
    
    [Header("State")]
    [SerializeField] private GlobalBaybladeData equipped;
    [CreateProperty] public GlobalBaybladeData Equipped {
        get => equipped;
        set => equipped = value;
    }
}