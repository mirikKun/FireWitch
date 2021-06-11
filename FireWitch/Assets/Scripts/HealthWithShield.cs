using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWithShield : HealthController
{
    [SerializeField] private float maxShieldStrength = 3;
    [SerializeField] private Image shieldBar;
    [SerializeField] private GameObject fireShield;
    [SerializeField] private FireShield shieldController;
    

    private float _shieldStrength;
    private bool _shieldStay ;
    
    
    public override void GetDamage(float damage)
    {
        if (_shieldStay)
            ShieldHit();
        else
            base.GetDamage(damage);
    }

    public void SetupShield()
    {
        _shieldStrength = maxShieldStrength;
        fireShield.SetActive(true);
        _shieldStay = true;
    }
    private void ShieldHit()
    {
        _shieldStrength--;
        if (shieldBar)
        {
            shieldBar.fillAmount = _shieldStrength / maxShieldStrength;
        }

        if (_shieldStrength <= 0)
        {
            shieldController.ShieldBreak();
            fireShield.SetActive(false);
            _shieldStay = false;
        }
    }
}