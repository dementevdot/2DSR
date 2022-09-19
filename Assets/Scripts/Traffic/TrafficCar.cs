using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    [SerializeField] private bool _isOpposite;

    private int _direction;
    private float _speed;

    public float Speed => _speed;

    private void Start()
    {
        if (_isOpposite)
            _direction = -1;
        else
            _direction = 1;

        Reset();
    }

    private void Update()
    {
        transform.position += new Vector3(_direction * _speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _speed = 0;
    }

    public void Reset()
    {
        _speed = 10;
        transform.rotation = new Quaternion(0,0,0,0);
    }
}
