using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : ObjectPool
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private Car _car;

    private int roadLength = 12;
    private int lastRoadIndex = 0;
    private int previousMileage = 12;
    private int roadSpawnPositionX = 28;

    private void Awake()
    {
        List<GameObject> _roadPrefabs = new List<GameObject>();

        _roadPrefabs.Add(_roadPrefab);

        Init(_roadPrefabs);
    }

    private void Start()
    {
        int spawnPositionX = -12;

        for (int i = 0; i < _capacity; i++)
        { 
            _pool[i].SetActive(true);
            _pool[i].transform.position = new Vector3(spawnPositionX, 0, transform.position.z);

            spawnPositionX += roadLength;
        }
    }

    private void Update()
    {
        if (_car.Mileage > previousMileage)
        {
            _pool[lastRoadIndex].transform.position = new Vector3(roadSpawnPositionX, 0, transform.position.z);

            previousMileage += roadLength;
            roadSpawnPositionX += roadLength;
            
            if (lastRoadIndex == _capacity - 1)
            {
                lastRoadIndex = 0;
            }
            else
            {
                lastRoadIndex++;
            }
        }
    }
}
