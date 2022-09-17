using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _braking;

    private float _mileage = 0;

    public float MaxSpeed => _maxSpeed;
    public float MinSpeed => _minSpeed;
    public float Acceleration => _acceleration;
    public float Bracking => _braking;
    public float Mileage => _mileage;

    public void AddMileage(float distance) 
    {
        _mileage += distance;
    }

    public void Destoy()
    {

    }

}
