using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected GameObject rootObject;

    private float _currentHealth;
    
    private void Start()
    {
        _currentHealth = maxHealth;
        
    }


    public virtual void GetDamage(float damage)
    {
        _currentHealth -= damage;
        if (healthBar)
            healthBar.fillAmount = _currentHealth / maxHealth;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(rootObject);
    }
}