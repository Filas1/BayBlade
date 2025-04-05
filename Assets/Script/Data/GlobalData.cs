using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "GameData/GlobalData")]
public class GlobalData : ScriptableObject, ISingleton
{
    [Header("Player Money")]
    [SerializeField] private ulong playerMoney;
    [CreateProperty] public ulong PlayerMoney
    {
        get => playerMoney;
        set => playerMoney = value;
    }

    [Header("Available bayblades")]
    [SerializeField] private GlobalBaybladeData[] bayblades;
    [CreateProperty] public GlobalBaybladeData[] Bayblades => bayblades;
    
    [Header("State")]
    [SerializeField] private GlobalBaybladeData viewedBayblade;
    [CreateProperty] public GlobalBaybladeData ViewedBayblade {
        get => viewedBayblade;
        set => viewedBayblade = value;
    }
    
    [SerializeField] private GlobalBaybladeData equipped;
    [CreateProperty] public GlobalBaybladeData Equipped {
        get => equipped;
        set => equipped = value;
    }
}