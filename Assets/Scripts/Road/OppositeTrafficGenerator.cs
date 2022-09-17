using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeTrafficGenerator : ObjectPool
{
    [SerializeField] private GameObject _car;
    [SerializeField] private float _minSecondsBetweenSpawn;
    [SerializeField] private float _maxSecondsBetweenSpawn;

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
                _secondsBetweenSpawn = Random.Range(_minSecondsBetweenSpawn, _maxSecondsBetweenSpawn);
                car.SetActive(true);
                car.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

                DisableObjectAbroadCamera();
            }
        }
    }
}
