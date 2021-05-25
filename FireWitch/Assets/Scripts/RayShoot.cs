using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject startFlash;
    [SerializeField] private GameObject hitFlash;
    private Quaternion _rotation;
    private Transform _transform;
    private bool _faceToRight;
    private List<ParticleSystem> _particles = new List<ParticleSystem>();
    private Transform _startFlashTransform;
    private Transform _hitFlashTransform;

    private void Start()
    {
        FillParticleList();
        _transform = transform;
        _startFlashTransform = startFlash.transform;
        _hitFlashTransform = hitFlash.transform;
        DisableRay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnableRay();
        }

        if (Input.GetKey(KeyCode.E))
        {
            UpdateRay();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DisableRay();
        }

        RotateToMouse();
    }

    private void EnableRay()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerMovement.RotateToFire(mousePos);
        lineRenderer.enabled = true;
        _faceToRight = _transform.position.x > mousePos.x;
        foreach (var particle in _particles)
            particle.Play();
    }

    private void UpdateRay()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, firePoint.position);
        _startFlashTransform.position = firePoint.position;
        
        lineRenderer.SetPosition(1, mousePos);
        

        var position = _transform.position;
        Vector2 direction = mousePos - (Vector2) position;
        RaycastHit2D hit = Physics2D.Raycast(position, direction.normalized, direction.magnitude, layerMask);
        if ((_faceToRight && _transform.position.x < mousePos.x) ||
            (!_faceToRight && _transform.position.x > mousePos.x))
        {
            playerMovement.RotateToFire(mousePos);
            _faceToRight = _transform.position.x > mousePos.x;
        }
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }

        _hitFlashTransform.position = lineRenderer.GetPosition(1);
    }

    private void DisableRay()
    {
        lineRenderer.enabled = false;
        foreach (var particle in _particles)
            particle.Stop();
    }

    private void RotateToMouse()
    {
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rotation.eulerAngles = new Vector3(0, 0, 180+angle);
        _transform.rotation = _rotation;
    }

    private void FillParticleList()
    {
        for (int i = 0; i < startFlash.transform.childCount; i++)
        {
            var particle = startFlash.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(particle!=null)
                _particles.Add(particle);
        }
        
        for (int i = 0; i < hitFlash.transform.childCount; i++)
        {
            var particle = hitFlash.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(particle!=null)
                _particles.Add(particle);
        }
    }
}