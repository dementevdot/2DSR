using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGenerator : ObjectPool
{
    [SerializeField] private GameObject _car;
    [SerializeField] private float _minSecondsBetweenSpawn;
    [SerializeField] private float _maxSecondsBetweenSpawn;
    [SerializeField] private float _minDistanceBetweenCars;

    private float _secondsBetweenSpawn;

    private void Awake()
    {
        List<GameObject> _carsPrefabs = new List<GameObject>();

        _carsPrefabs.Add(_car);

        Init(_carsPrefabs);

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
                        if (Vector2.Distance(item.transform.position, transform.position) < _minDistanceBetweenCars)
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
            car.GetComponent<TrafficCar>().Reset();
        }
    }

    public override void ResetPool()
    {
        base.ResetPool();

        ResetAllCars();
    }


}
