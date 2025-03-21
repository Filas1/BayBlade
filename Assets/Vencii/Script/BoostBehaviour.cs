using System;
using UnityEngine;

public class BoostBehaviour : MonoBehaviour
{
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BeyBladeBehaviour>(out var bayblade))
        {
            bayblade.ApplyBoost(400);
        }
    }
}
