using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    [SerializeField] private int maxMana;
    [SerializeField] private Image manaBar;

    private int _currentMana;

    public bool CheckManaCount(int manaCost)
    {
        bool enoughOfMana = _currentMana - manaCost > 0;
        return enoughOfMana;
    }
    public void SpendMana(int manaCost)
    {
        _currentMana -= manaCost;
    }
}
