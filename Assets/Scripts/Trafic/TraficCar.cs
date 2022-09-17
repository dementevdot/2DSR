using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficCar : MonoBehaviour
{
    [SerializeField] private bool _isOpposite;

    private int _direction;

    private void Start()
    {
        if (_isOpposite)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
    }
    private void Update()
    {
        transform.position += new Vector3(_direction * 10f * Time.deltaTime, 0);
    }
}
