using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireShield : Skill
{
    [SerializeField] private float maxShieldStrength = 3;
    [SerializeField] private Image shieldBar;
    [SerializeField] private HealthWithShield healthShield;

    private float _shieldStrength;
    private bool _fireShieldActive;
    
    private void Start()
    {
        _shieldStrength = maxShieldStrength;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&&available)
        {
            ActivateSkill();
        }
    }

    public override void ActivateSkill()
    {
        if(!available)
            return;
        healthShield.SetupShield();
        _fireShieldActive = !_fireShieldActive;
        skillObject.SetActive(_fireShieldActive);
    }

    public void ShieldBreak()
    {
        StartCoroutine(ShieldReload());
    }

    private IEnumerator ShieldReload()
    {
        available = false;
        yield return new WaitForSeconds(reloadTime);
        available = true;
    }


}
