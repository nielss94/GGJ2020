using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Notify systemmanager for damage
            Destroy(other.gameObject);
            // AudioManager.instance.PlayCrashSound(0);
        }
    }
}
