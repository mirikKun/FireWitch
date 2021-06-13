using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private AIPath aiPath;

    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((aiPath.desiredVelocity.x >= 0.01f && _transform.localScale.x > 0) ||
            (aiPath.desiredVelocity.x <= -0.01f && _transform.localScale.x < 0))
        {
            var localScale = _transform.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            _transform.localScale = localScale;
        }
    }
}