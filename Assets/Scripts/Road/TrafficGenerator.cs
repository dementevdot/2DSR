using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGenerator : ObjectPool
{
    [SerializeField] private GameObject _trafficCarPrefab;
    [SerializeField] private float _minSecondsBetweenSpawn;
    [SerializeField] private float _maxSecondsBetweenSpawn;
    [SerializeField] private float _minDistanceBetweenCars;
    [SerializeField] private float _maxDistanceBetweenCars;

    private float _secondsBetweenSpawn;

    private void Awake()
    {
        List<GameObject> _trafficCarPrefabs = new List<GameObject>();

        _trafficCarPrefabs.Add(_trafficCarPrefab);

        Init(_trafficCarPrefabs);

        _secondsBetweenSpawn = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);
    }

    private void Update()
    {
        _secondsBetweenSpawn -= Time.deltaTime;

        if (_secondsBetweenSpawn < 0)
        {
            if (TryGetObject(out GameObject car))
            {
                if (_minDistanceBetweenCars > 0)
                {
                    List<GameObject> activeOjects = GetActiveObjects();
                    
                    foreach (var item in activeOjects)
                    {
                        if (Vector2.Distance(item.transform.position, transform.position) < Random.Range(_minDistanceBetweenCars, _maxDistanceBetweenCars))
                            return;
                    }
                }

                _secondsBetweenSpawn = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);

                car.SetActive(true);
                car.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                DisableObjectAbroadCamera();
            }
        }
    }

    private void ResetAllCars()
    {
        foreach(var car in Pool)
        {
            if (car.TryGetComponent<TrafficCar>(out TrafficCar trafficCar))
                trafficCar.Reset();
        }
    }

    public override void ResetPool()
    {
        base.ResetPool();

        ResetAllCars();
    }
}
