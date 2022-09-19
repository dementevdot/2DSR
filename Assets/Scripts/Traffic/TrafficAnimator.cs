using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _frontWheel;
    [SerializeField] private GameObject _rearWheel;
    [SerializeField] private TrafficCar _trafficCar;
    [SerializeField] private bool _isOpposite;

    private float _wheelRotation = 0;
    private int _ratio = 150;
    private int _direction;

    private void Start()
    {
        if (_isOpposite)
            _direction = 1;
        else
            _direction = -1;
    }

    private void Update()
    {
        _wheelRotation = _trafficCar.Speed * _ratio * Time.deltaTime * _direction;

        _frontWheel.transform.Rotate(0, 0, -_wheelRotation);
        _rearWheel.transform.Rotate(0, 0, -_wheelRotation);
    }
}
