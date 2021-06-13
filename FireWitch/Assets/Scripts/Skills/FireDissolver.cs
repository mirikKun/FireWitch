using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDissolver : Skill
{
    [SerializeField] private float timeToFade = 2f;

    private Material _material;
    private bool _isConverting;
    private bool _dissolved;
    private float _fadingTime;
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private void Start()
    {
        _fadingTime = timeToFade;
        _material = GetComponent<SpriteRenderer>().material;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_isConverting)
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
        _isConverting = true;
        StartCoroutine(_dissolved ? Appearance() : Dissolving());
    }
    private IEnumerator Dissolving()
    {
        
        while (_fadingTime > 0f)
        {
            _fadingTime -= Time.deltaTime;
            _material.SetFloat(Fade, _fadingTime / timeToFade);
            yield return null;
        }
        _fadingTime = 0;
        _isConverting = false;
        _dissolved = true;
    }
    private IEnumerator Appearance()
    {
        available = false;
        while (_fadingTime < timeToFade)
        {
            _fadingTime += Time.deltaTime;
            _material.SetFloat(Fade, _fadingTime / timeToFade);
            yield return null;
        }
        _fadingTime = timeToFade;
        _isConverting = false;
        _dissolved = false;
        yield return new WaitForSeconds(reloadTime);
        available = true;
    }
}
