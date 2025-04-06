using System.Collections;
using UnityEngine;

public class BeyBladeBehaviour : MonoBehaviour, IInteractor
{

    [SerializeField] protected BayBladeData Data;
    public float Speed => Data.Speed;
    
    protected float CurrentSpeed;
    protected float currentAcceleration;
    protected Rigidbody Rb;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        CurrentSpeed = Data.StartSpeed;
    }

    protected virtual void FixedUpdate()
    {
        if (CurrentSpeed < Data.MinSpeed)
        {
            enabled=false;
            return;
        }
        // Apply spinning torque
        Rb.AddRelativeTorque(Data.SpinAxis * (CurrentSpeed * Time.fixedDeltaTime), ForceMode.Acceleration);
            
        // Decrease speed over time
        CurrentSpeed = Mathf.Max(CurrentSpeed - Data.SpinDeceleration * Time.fixedDeltaTime, Data.MinSpeed);

        // Add wobble effect as speed decreases
        var wobbleAmount = Mathf.Lerp(0f, Data.MaxTiltAngle, 1 - (CurrentSpeed / Data.StartSpeed));
        transform.rotation = Quaternion.Euler(
            Mathf.Sin(Time.time * 5f) * wobbleAmount,
            transform.rotation.eulerAngles.y,
            Mathf.Cos(Time.time * 5f) * wobbleAmount
        );

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BeyBladeBehaviour>(out var otherBeyBlade))
        {
            
            var collisionDirection = (transform.position - collision.transform.position).normalized;

            if (CurrentSpeed < otherBeyBlade.Speed)
            {
                HandleCollision(-collisionDirection, otherBeyBlade.Speed);
            }
            else
            {
                otherBeyBlade.HandleCollision(-collisionDirection, CurrentSpeed);
            }


            // Reduce speed on collision
            CurrentSpeed *= 0.8f;
        }
    }

    public void HandleCollision(Vector3 direction, float otherSpeed)
    {
        Rb.AddForce(direction * Data.CollisionForce * (otherSpeed / Data.StartSpeed), ForceMode.Impulse);
        CurrentSpeed *= 0.8f;
    }

    public float GetCurrentSpeed()
    {
        return CurrentSpeed;
    }

    public void ApplyBoost(float boostAmount)
    {
        CurrentSpeed += boostAmount;
    }
}