using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalBaybladeData : ScriptableObject
{
    [Header("Desc")]
    [SerializeField] private string baybladeName;
    [CreateProperty] public string Name => baybladeName;
    [SerializeField] private string description;
    [CreateProperty] public string Description => description;
    [SerializeField] private uint price;
    [CreateProperty] public uint Price => price;
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [SerializeField] private BayBladeData data;
    [CreateProperty] public BayBladeData Data => data;
    
    [Header("StatLock")]
    [SerializeField] private float maxDefense = 120f;
    [SerializeField] private float maxAttack = 100f;
    [SerializeField] private float maxStamina= 90f;
    
    [CreateProperty] public float MaxDefense => maxDefense;
    [CreateProperty] public float MaxAttack => maxAttack;
    [CreateProperty] public float MaxStamina => maxStamina;
    
    [Header("State")]
    [field: SerializeField] public bool Owned { get; set; }

}