using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : ObjectPool
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private Car _car;

    private int roadLength;
    private int lastRoadIndex;
    private int previousMileage;
    private int roadSpawnPositionX;

    private void Awake()
    {
        List<GameObject> _roadPrefabs = new List<GameObject>();

        _roadPrefabs.Add(_roadPrefab);

        Init(_roadPrefabs);
    }

    private void Start()
    {
        int spawnPositionX = -12;

        roadLength = 12;
        lastRoadIndex = 0;
        previousMileage = 12;
        roadSpawnPositionX = 28;

        for (int i = 0; i < Capacity; i++)
        {
            Pool[i].SetActive(true);
            Pool[i].transform.position = new Vector3(spawnPositionX, 0, transform.position.z);

            spawnPositionX += roadLength;
        }
    }

    private void Update()
    {
        if (_car.Mileage > previousMileage)
        {
            Pool[lastRoadIndex].transform.position = new Vector3(roadSpawnPositionX, 0, transform.position.z);

            previousMileage += roadLength;
            roadSpawnPositionX += roadLength;
            
            if (lastRoadIndex == Capacity - 1)
            {
                lastRoadIndex = 0;
            }
            else
            {
                lastRoadIndex++;
            }
        }
    }

    public override void ResetPool()
    {
        base.ResetPool();

        Start();
    }
}
