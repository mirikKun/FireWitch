using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJump : StateMachineBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float fallMultiplier = 2.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
  
        _rb.velocity += Vector2.up * Physics2D.gravity * ((fallMultiplier - 1) * Time.deltaTime);
    }
}
