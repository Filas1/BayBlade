using System.Collections;
using UnityEngine;

public class BeyBladeBehaviour : MonoBehaviour
{
    [Header("Spinning Settings")]
    [SerializeField] private float minSpeed = 10f;
    [SerializeField] private float startSpeed = 20f;
    [SerializeField] private float spinDeceleration = 1f;
    [SerializeField] private float maxTiltAngle = 15f;
    [SerializeField] private Vector3 spinAxis = Vector3.up;

    [Header("Collision Settings")]
    [SerializeField] private float collisionForce = 500f;
    [SerializeField] private float bounceForce = 100f;

    [SerializeField] protected float MoveSpeed = 10f;
    [SerializeField] protected float MaxMoveVelocity = 8f;
    [SerializeField] protected float MoveVelocity = 8f;
    
    private float currentSpeed;
    protected Rigidbody Rb;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        currentSpeed = startSpeed;
        StartCoroutine(SpinningBehaviour());
    }


    
    private IEnumerator SpinningBehaviour()
    {
        while (currentSpeed > minSpeed)
        {
            // Apply spinning torque
            Rb.AddRelativeTorque(spinAxis * (currentSpeed * Time.fixedDeltaTime), ForceMode.Force);
            
            // Decrease speed over time
            currentSpeed = Mathf.Max(currentSpeed - spinDeceleration * Time.fixedDeltaTime, minSpeed);

            // Add wobble effect as speed decreases
            float wobbleAmount = Mathf.Lerp(0f, maxTiltAngle, 1 - (currentSpeed / startSpeed));
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
            Rb.AddForce(collisionDirection * collisionForce * (currentSpeed / startSpeed), ForceMode.Impulse);
            otherBeyBlade.HandleCollision(-collisionDirection, currentSpeed);

            // Reduce speed on collision
            currentSpeed *= 0.8f;
        }
    }

    public void HandleCollision(Vector3 direction, float otherSpeed)
    {
        Rb.AddForce(direction * collisionForce * (otherSpeed / startSpeed), ForceMode.Impulse);
        currentSpeed *= 0.8f;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void ApplyBoost(float boostAmount)
    {
        currentSpeed += boostAmount;
    }
}