using System;
using Unity.VisualScripting;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour, ISingleton
{
    [SerializeField] private float areaRadius = 14.5f;
    [SerializeField] private GameObject boosterPrefab;

    public static BoosterSpawner spawner { get; private set; }
    private void Start()
    {
        
    }

    private void Awake()
    {
        spawner = this;
    }
}
