using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class CarCollisionHandler : MonoBehaviour
{
    [SerializeField] private Car _car;

    private void Awake()
    {
        _car = GetComponent<Car>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
