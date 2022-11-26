using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car))]
[RequireComponent(typeof(CarMover))]
public class CarCollisionHandler : MonoBehaviour
{
    [SerializeField] private ScreenController _screenController;

    private CarMover _carMover;

    private void Awake()
    {
        _carMover = GetComponent<CarMover>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _screenController.EndGame();
        _carMover.DisableCarMoving();
    }
}
