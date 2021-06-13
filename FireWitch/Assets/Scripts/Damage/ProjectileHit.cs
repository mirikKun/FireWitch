using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    [SerializeField] private GameObject boomParticle;
    [SerializeField] private float secondsToDestroy=4;

    [SerializeField] private int damage=10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyByTime());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<HealthController>()?.GetDamage(damage);
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
