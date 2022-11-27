using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : ObjectPool
{
    private GameObject _roadPrefab;
    private Car _car;
    private int _roadLength;
    private int _lastRoadIndex;
    private int _previousMileage;
    private int _roadSpawnPositionX;

    private void Start()
    {
        int spawnPositionX = -12;

        _roadLength = 12;
        _lastRoadIndex = 0;
        _previousMileage = 12;
        _roadSpawnPositionX = 28;

        for (int i = 0; i < Capacity; i++)
        {
            Pool[i].SetActive(true);
            Pool[i].transform.position = new Vector3(spawnPositionX, 0, transform.position.z);

            spawnPositionX += _roadLength;
        }
    }

    private void Update()
    {
        if (_car.Mileage > _previousMileage)
        {
            Pool[_lastRoadIndex].transform.position = new Vector3(_roadSpawnPositionX, 0, transform.position.z);

            _previousMileage += _roadLength;
            _roadSpawnPositionX += _roadLength;
            
            if (_lastRoadIndex == Capacity - 1)
            {
                _lastRoadIndex = 0;
            }
            else
            {
                _lastRoadIndex++;
            }
        }
    }

    public void Init(GameObject roadPrefab, Car car, GameObject pool)
    {
        _roadPrefab = roadPrefab;
        _car = car;
        PoolGameObject = pool;

        List<GameObject> _roadPrefabs = new List<GameObject>();

        _roadPrefabs.Add(_roadPrefab);

        Init(_roadPrefabs);
    }

    public override void ResetPool()
    {
        base.ResetPool();

        Start();
    }
}
