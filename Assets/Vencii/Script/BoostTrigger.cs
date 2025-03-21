using System;
using UnityEngine;

public class BoostTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BeyBladeBehaviour>(out var bayblade))
        {
            bayblade.ApplyBoost(400);
        }
    }
}
