using UnityEngine;

public class AreaMovement : MonoBehaviour
{
    [SerializeField] private float maxAreaTilt = 20f;
    [SerializeField] private float areaTiltSpeed = 1f;

    private void Update()
    {
        // Rotate horizontally
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        transform.Rotate(Vector3.up * (moveHorizontal * Time.deltaTime * 10f));
        
        // Tilt left
        if (Input.GetButton("TiltLeft"))
        {
            // Calculate the next potential rotation
            Quaternion tiltLeft = transform.rotation * Quaternion.Euler(0, 0, areaTiltSpeed * Time.deltaTime * 10f);
            
            // Check if the tilt is within the max tilt range
            float currentTilt = tiltLeft.eulerAngles.z > 180 ? tiltLeft.eulerAngles.z - 360 : tiltLeft.eulerAngles.z;
            if (Mathf.Abs(currentTilt) <= maxAreaTilt)
            {
                transform.rotation = tiltLeft;
            }
        }
        
        // Tilt right
        if (Input.GetButton("TiltRight"))
        {
            // Calculate the next potential rotation
            Quaternion tiltRight = transform.rotation * Quaternion.Euler(0, 0, areaTiltSpeed * Time.deltaTime * -10f);
            
            // Check if the tilt is within the max tilt range
            float currentTilt = tiltRight.eulerAngles.z > 180 ? tiltRight.eulerAngles.z - 360 : tiltRight.eulerAngles.z;
            if (Mathf.Abs(currentTilt) <= maxAreaTilt)
            {
                transform.rotation = tiltRight;
            }
        }
        
        // Reset tilt
        if (Input.GetButton("ResetTilt"))
        {
            // Smoothly interpolate back to zero rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
        }
    }
}