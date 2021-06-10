using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject rootObject;

    private float _health;
    
    void Start()
    {
        _health = maxHealth;
    }


    public void GetDamage(float damage)
    {
        _health -= damage;
        if (healthBar)
            healthBar.fillAmount = _health / maxHealth;
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Destroy(rootObject);
    }
}