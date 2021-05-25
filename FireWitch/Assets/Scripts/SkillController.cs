using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private GameObject fireGround;
    [SerializeField] private GameObject fireShield;
    [SerializeField] private float timeToFade = 2f;
    private Material _material;
    private bool _isConverting;
    private bool _fireShieldActive;
    private bool _dissolved;
    private float _fade;
    private static readonly int Fade = Shader.PropertyToID("_Fade");

    void Start()
    {
        _fade = timeToFade;
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&!_isConverting)
        {
            _isConverting = true;
            StartCoroutine(_dissolved ? Appearance() : Dissolving());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(fireGround, transform.position, quaternion.identity);
            GameObject fireGroundMove=Instantiate(fireGround, transform.position, quaternion.identity);
            fireGroundMove.GetComponent<FireGroundMove>().ChangeDirection();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _fireShieldActive = !_fireShieldActive;
            fireShield.SetActive(_fireShieldActive);
        }
    }

    private IEnumerator Dissolving()
    {
        while (_fade > 0f)
        {
            _fade -= Time.deltaTime;
            _material.SetFloat(Fade, _fade / timeToFade);
            yield return null;
        }
        _fade = 0;
        _isConverting = false;
        _dissolved = true;
    }
    private IEnumerator Appearance()
    {
        while (_fade < timeToFade)
        {
            _fade += Time.deltaTime;
            _material.SetFloat(Fade, _fade / timeToFade);
            yield return null;
        }
        _fade = timeToFade;
        _isConverting = false;
        _dissolved = false;
    }
}