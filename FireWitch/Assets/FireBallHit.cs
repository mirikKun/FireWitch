using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallHit : MonoBehaviour
{
    [SerializeField] GameObject boomParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(boomParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
