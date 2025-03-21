using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Serialization;
[CreateAssetMenu(fileName = "BayBladeData", menuName = "ScriptableObjects/BayBladeData", order = 1)]
public class BayBladeData : ScriptableObject
{
    [field: SerializeField] public float MinSpeed { get; private set; } = 10f;
    [field: SerializeField] public float StartSpeed { get; private set; } = 20f;
    [field: SerializeField] public float SpinDeceleration { get; private set; } = 1f;
    [field: SerializeField] public float MaxTiltAngle { get; private set; } = 15f;
    [field: SerializeField] public Vector3 SpinAxis { get; private set; } = Vector3.up;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f ;
    [field: SerializeField] public float MaxMoveVelocity { get; private set; } = 8f;
    [field: SerializeField] public float CollisionForce { get; private set; } = 10f;
    [field: SerializeField] public float BounceForce { get; private set; } = 10f;
}