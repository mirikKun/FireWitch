using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGroundDamager : MonoBehaviour
{
    [SerializeField] private float damage = 6;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<HealthController>()?.GetDamage(damage);
    }
}
