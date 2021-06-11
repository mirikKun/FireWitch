using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool available = true;
    [SerializeField] protected float manaCost;
    [SerializeField] protected GameObject skillObject;

    public abstract void ActivateSkill();

}
