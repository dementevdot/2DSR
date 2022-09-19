using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _frontWheel;
    [SerializeField] private GameObject _rearWheel;
    [SerializeField] private Car _car;

    private float _wheelRotation = 0;
    private int _ratio = 200;

    private void Update()
    {
        _wheelRotation = _car.CurrentSpeed * _ratio * Time.deltaTime;

        _frontWheel.transform.Rotate(0,0,-_wheelRotation);
        _rearWheel.transform.Rotate(0,0,-_wheelRotation);
    }
}
