using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Prefabs _prefabs;
    
    private CameraFollowing _cameraFollowing;
    private const float  _cameraFollowingXOffset = 4;
    private Car _car;

    private void Awake()
    {
        Instantiate(_prefabs.Camera);
        Instantiate(_prefabs.Canvas);
        Instantiate(_prefabs.Car);

        if (_prefabs.Camera.TryGetComponent<CameraFollowing>(out CameraFollowing cameraFollowing) == false)
        {
            throw new NullReferenceException();
        }
        else
        {
            _cameraFollowing = cameraFollowing;
            _cameraFollowing.Init(_prefabs.Car, _cameraFollowingXOffset);
        }

        if (_prefabs.Car.TryGetComponent<Car>(out Car car) == false)
        {
            throw new NullReferenceException();
        }
        else
        {
            _car = car;
            //_car.Init();
        }


    }



    [System.Serializable]
    private class Prefabs
    {
        public GameObject Camera;
        public GameObject Canvas;
        public GameObject Car;
    }
}

