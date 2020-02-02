using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private ParticleSystem[] muzzleFlashes = null;
    [SerializeField]
    private Transform[] barrelTransforms = null;
    [SerializeField]
    private float shootDelay = 0.75f;
    
    private bool _startShooting = false;
    private bool _canShoot = true;

    private void Update()
    {
        if (!(_startShooting && _canShoot)) return;
        
        Shoot();
    }
    
    private void Shoot()
    {
        PlayMuzzleFlashes();
        SpawnBullets();

        StartCoroutine(DelayShooting());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _startShooting = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _startShooting = false;
    }
    
    private void PlayMuzzleFlashes()
    {
        foreach (ParticleSystem muzzleFlash in muzzleFlashes)
        {
            muzzleFlash.Emit(1);
        }
    }
    
    private void SpawnBullets()
    {
        foreach (Transform barrelTransform in barrelTransforms)
        {
            Instantiate(bullet, barrelTransform.position, barrelTransform.rotation * Quaternion.Euler(90, -90, 0), barrelTransform);
        }
    }
    
    
    private IEnumerator DelayShooting()
    {
        _canShoot = false;
        
        yield return new WaitForSeconds(shootDelay);

        _canShoot = true;
    }
}