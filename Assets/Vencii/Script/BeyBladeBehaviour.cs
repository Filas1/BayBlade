using System.Collections;
using UnityEngine;

public class BeyBladeBehaviour : MonoBehaviour
{

    [SerializeField] protected BayBladeData Data;
    [SerializeField] protected float MoveVelocity = 8f;
    
    protected float CurrentSpeed;
    protected Rigidbody Rb;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        CurrentSpeed = Data.StartSpeed;
        StartCoroutine(SpinningBehaviour());
    }


    
    private IEnumerator SpinningBehaviour()
    {
        while (CurrentSpeed > Data.MinSpeed)
        {
            // Apply spinning torque
            Rb.AddRelativeTorque(Data.SpinAxis * (CurrentSpeed * Time.fixedDeltaTime), ForceMode.Force);
            
            // Decrease speed over time
            CurrentSpeed = Mathf.Max(CurrentSpeed - Data.SpinDeceleration * Time.fixedDeltaTime, Data.MinSpeed);

            // Add wobble effect as speed decreases
            float wobbleAmount = Mathf.Lerp(0f, Data.MaxTiltAngle, 1 - (CurrentSpeed / Data.StartSpeed));
            transform.rotation = Quaternion.Euler(
                Mathf.Sin(Time.time * 5f) * wobbleAmount,
                transform.rotation.eulerAngles.y,
                Mathf.Cos(Time.time * 5f) * wobbleAmount
            );

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BeyBladeBehaviour>(out var otherBeyBlade))
        {
            
            Vector3 collisionDirection = (transform.position - collision.transform.position).normalized;
            
            // Apply force to both BeyBlades
            Rb.AddForce(collisionDirection * Data.CollisionForce * (CurrentSpeed / Data.StartSpeed), ForceMode.Impulse);
            otherBeyBlade.HandleCollision(-collisionDirection, CurrentSpeed);

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