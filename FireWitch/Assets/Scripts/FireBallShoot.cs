using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireBallSpeed=10;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float smallFireBallSpeed=20;
    [SerializeField] private GameObject smallFireBall;
    
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot(fireBall,fireBallSpeed);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(smallFireBall,smallFireBallSpeed);
        }
    }

    private void Shoot(GameObject projectile,float speed)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        var position = _transform.position;
        Vector2 direction = mousePos - (Vector2) position;
        GameObject currentFireBall=Instantiate(projectile, firePoint.position, Quaternion.identity);
        currentFireBall.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }
}
