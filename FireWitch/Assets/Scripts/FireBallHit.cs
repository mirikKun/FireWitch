using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallHit : MonoBehaviour
{
    [SerializeField] private GameObject boomParticle;
    [SerializeField] private float secondsToDestroy=4;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyByTime());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Boom();
    }

    private void Boom()
    {
        Instantiate(boomParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Boom();
    }
}
