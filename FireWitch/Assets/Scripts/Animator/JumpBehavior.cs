using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    
    [SerializeField] private float speed=5;

    private Transform _playerPosition;

    private Rigidbody2D _rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rb = animator.GetComponent<Rigidbody2D>();
        _playerPosition = FindObjectOfType<PlayerMovement>().transform;

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_rb.velocity.y<0)
        {
            animator.SetTrigger("EndJump");
        }
    

        Vector2 target = new Vector2(_playerPosition.position.x, _rb.position.y);
        _rb.position = Vector2.MoveTowards(_rb.position, target, speed * Time.deltaTime);
    }


}
