using System;
using UnityEngine;

public class BoostBehaviour : MonoBehaviour
{
    private float SpinSpeed = 4f;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * SpinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boost aquired");
        if (other.TryGetComponent<BeyBladeBehaviour>(out var bayblade))
        {
            bayblade.ApplyBoost(400);
            Destroy(gameObject);
        }
    }
}
