using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorByTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1;
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
