using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private GameObject impactParticle = null;

    [SerializeField]
    private float bulletForce = 20f;
    [SerializeField]
    private float lifeTime = 5f;

    private void Start()
    {
        rb.AddForce(transform.up * bulletForce);

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.CompareTag("BulletImpact") && CompareTag("EnemyBullet"))) return;

        DebugGUI.Log("hit");

        Instantiate(impactParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}