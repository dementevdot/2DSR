using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _braking;
    [SerializeField] private float _handleability;

    private Vector3 _startPosition;
    private float _currentSpeed = 0;
    private float _mileage = 0;

    public float MaxSpeed => _maxSpeed;
    public float MinSpeed => _minSpeed;
    public float Acceleration => _acceleration;
    public float Bracking => _braking;
    public float Handleabitity => _handleability;
    public float Mileage => _mileage;
    public float CurrentSpeed => _currentSpeed;

    public event UnityAction CarReset;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void Reset()
    {
        _currentSpeed = 0;
        _mileage = 0;
        transform.position = _startPosition;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        CarReset?.Invoke();
    }
    public void AddMileage(float distance) 
    {
        _mileage += distance;
    }

    public void SetCurrentSpeed(float speed)
    {
        if (speed > _minSpeed && speed < _maxSpeed)
            _currentSpeed = speed;
    }

    public void IncreaseSpeed(float speed)
    {
        if (speed > 0)
            if (_currentSpeed + speed <= _maxSpeed)
                _currentSpeed += speed;
    }

    public void DecreaseSpeed(float speed)
    {
        if (speed > 0)
            if (_currentSpeed - speed >= _minSpeed)
                _currentSpeed -= speed;
    }
}
