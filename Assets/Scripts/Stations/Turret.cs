using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : Station
{
    [SerializeField]
    private float rotateSpeed = 2f;
    [SerializeField]
    private float maxRotation = 0.67f;
    [SerializeField]
    private float minRotation = 200f;
    [SerializeField]
    private bool reverseMovement = false;
    [SerializeField]
    private float shootDelay = 1f;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private ParticleSystem[] muzzleFlashes;
    [SerializeField]
    private Transform[] barrelTransforms;
    
    

    private float _yMovement;
    private float _xRotation;
    private bool _canShoot = true;

    private void Update()
    {
        base.Update();
        
        float turnSpeed = rotateSpeed * Time.deltaTime;
        float rotate = -(_yMovement * turnSpeed);

        if (reverseMovement)
        {
            rotate = -rotate;
        }

        if (rotate > 0 && transform.eulerAngles.y < maxRotation || rotate < 0 && transform.eulerAngles.y > minRotation)
            transform.Rotate(Vector3.forward * rotate);
    }

    public override void ProcessInput(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _yMovement = input.y;
    }

    public void Shoot()
    {
        if(!_canShoot) return;

        PlayMuzzleFlashes();
        SpawnBullets();

        StartCoroutine(DelayShooting());
    }

    private void SpawnBullets()
    {
        foreach (Transform barrelTransform in barrelTransforms)
        {
            Instantiate(bullet, barrelTransform.position, barrelTransform.rotation * Quaternion.Euler(90, -90, 0));
        }
    }

    private void PlayMuzzleFlashes()
    {
        foreach (ParticleSystem muzzleFlash in muzzleFlashes)
        {
            muzzleFlash.Emit(1);
        }
    }

    private IEnumerator DelayShooting()
    {
        _canShoot = false;
        
        yield return new WaitForSeconds(shootDelay);

        _canShoot = true;
    }
}