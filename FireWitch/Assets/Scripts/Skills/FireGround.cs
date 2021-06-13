using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireGround : Skill
{
     private FireGroundMove fireGround;

    private void Start()
    {
        fireGround = skillObject.GetComponent<FireGroundMove>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateSkill();
        }
    }

    public override void ActivateSkill()
    {
        if(!available)
            return;
        if(!manaController.TrySpendMana(manaCost))
            return;
        Instantiate(fireGround, transform.position, quaternion.identity);
        FireGroundMove fireGroundMove=Instantiate(fireGround, transform.position, quaternion.identity);
        fireGroundMove.ChangeDirection();
        StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        available = false;
        yield return new WaitForSeconds(reloadTime);
        available = true;
    }
}