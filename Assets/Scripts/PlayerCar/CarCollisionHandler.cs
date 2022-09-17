using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
public class CarCollisionHandler : MonoBehaviour
{
    private Car _car;
    private CarMover _carMover;

    private void Awake()
    {
        _car = GetComponent<Car>();
        _carMover = GetComponent<CarMover>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _car.InvokeGameOver();
        _carMover.DisableCarMoving();
    }
}
