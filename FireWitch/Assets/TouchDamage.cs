using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<HealthController>()?.GetDamage(damage);
    }
}
