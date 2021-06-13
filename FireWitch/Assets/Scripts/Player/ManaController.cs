using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    [SerializeField] private float maxMana=100;
    [SerializeField] private Image manaBar;
    [SerializeField] private float manaRegen=3;

    private float _currentMana;

    private void Start()
    {
        _currentMana = maxMana;
    }

    private void Update()
    {
        if (_currentMana >= maxMana)
            return;

        _currentMana += manaRegen * Time.deltaTime;
        if (_currentMana > maxMana)
            _currentMana = maxMana;
        if (manaBar)
            manaBar.fillAmount =  _currentMana / maxMana;
    }

    public bool TrySpendMana(int manaCost)
    {
        bool enoughOfMana = _currentMana - manaCost > 0;
        if (enoughOfMana)
        {
            _currentMana -= manaCost;

            if (manaBar)
                manaBar.fillAmount = (int) _currentMana / maxMana;
        }

        return enoughOfMana;
    }
}