using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplayer;
    [SerializeField] private bool infiniteHorizontal;
    private Transform _camera;
    private Vector3 _lastCameraPosition;
    private Transform _transform;
    private float _textureUnitSizeX;

    void Start()
    {
        _transform = transform;
        _camera = FindObjectOfType<CinemachineBrain>().transform;
        _lastCameraPosition = _camera.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = _camera.position - _lastCameraPosition;
        _transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplayer,0);
        _lastCameraPosition = _camera.position;

        if (infiniteHorizontal && Mathf.Abs(_camera.position.x - _transform.position.x) >= _textureUnitSizeX)
        {
            float offset = (_camera.position.x - _transform.position.x) % _textureUnitSizeX;
            _transform.position = new Vector3(_camera.position.x + offset, _transform.position.y);
        }
    }
}