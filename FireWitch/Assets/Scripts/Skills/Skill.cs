using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool available = true;
    [SerializeField] protected int manaCost;
    [SerializeField] protected ManaController manaController;
    [SerializeField] protected GameObject skillObject;

    public abstract void ActivateSkill();

}
