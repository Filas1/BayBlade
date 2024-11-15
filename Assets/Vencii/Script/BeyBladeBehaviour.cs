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
    [SerializeField] private float bounceForce = 300f;
    
    private float currentSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = startSpeed;
        StartCoroutine(SpinningBehaviour());
    }


    
    private IEnumerator SpinningBehaviour()
    {
        while (currentSpeed > minSpeed)
        {
            // Apply spinning torque
            rb.AddRelativeTorque(spinAxis * (currentSpeed * Time.fixedDeltaTime), ForceMode.Force);
            
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
            // Calculate collision direction
            Vector3 collisionDirection = (transform.position - collision.transform.position).normalized;
            
            // Apply force to both BeyBlades
            rb.AddForce(collisionDirection * collisionForce * (currentSpeed / startSpeed), ForceMode.Impulse);
            otherBeyBlade.HandleCollision(-collisionDirection, currentSpeed);

            // Reduce speed on collision
            currentSpeed *= 0.8f;
        }
    }

    public void HandleCollision(Vector3 direction, float otherSpeed)
    {
        rb.AddForce(direction * collisionForce * (otherSpeed / startSpeed), ForceMode.Impulse);
        currentSpeed *= 0.8f;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}