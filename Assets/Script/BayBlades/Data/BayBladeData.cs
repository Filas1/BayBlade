using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "BayBladeData", menuName = "GameData/BayBladeData", order = 1)]
public class BayBladeData : ScriptableObject
{
    [Header("Spinning Behaviour")]
    [field: SerializeField] public float MinSpeed { get; private set; } = 10f;
    [field: SerializeField] public float StartSpeed { get; private set; } = 20f;
    [field: SerializeField] public float SpinDeceleration { get; private set; } = 1f;
    [field: SerializeField] public float MaxTiltAngle { get; private set; } = 15f;
    [field: SerializeField] public Vector3 SpinAxis { get; private set; } = Vector3.up;
    [Header("Movement Behaviour")]
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f ;
    [field: SerializeField] public float MaxMoveVelocity { get; private set; } = 8f;
    [Header("Collision Behaviour")]
    [field: SerializeField] public float CollisionForce { get; private set; } = 10f;
    [field: SerializeField] public float BounceForce { get; private set; } = 10f;
    [Header("Stats")]
    [field: SerializeField] public float Defense { get; private set; } = 60f;
    [field: SerializeField] public float Attack { get; private set; } = 50f;
    [field: SerializeField] public float Stamina { get; private set; } = 40f;
    [field: SerializeField] public float Speed { get; private set; } = 20f;
    [Header("Props")]
    [field: SerializeField] public uint Cost { get; private set; } = 1000;
    [Header("Abilities")]
    [field: SerializeField, CanBeNull] public Ability MandatoryAbility { get; private set; }
    [field: SerializeField, CanBeNull] public Ability SecondaryAbility { get; private set; }
    [field: SerializeField, CanBeNull] public Ability OtherAbility { get; private set; }
}