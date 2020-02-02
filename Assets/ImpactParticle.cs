using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticle : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
