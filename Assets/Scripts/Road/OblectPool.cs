using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] protected int _capacity;

    protected Camera _camera;

    protected List<GameObject> _pool = new List<GameObject>();

    protected void Init(List<GameObject> prefabs)
    {
        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefabs[Random.Range(0, prefabs.Count - 1)], _spawner.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(road => road.activeSelf == false);

        return result != null;
    }

    protected void DisableObjectAbroadCamera()
    {
        Vector3 disaplePoint = _camera.ViewportToWorldPoint(new Vector2(-1, 0));

        foreach (var item in _pool)
        {
            if (item.activeSelf == true)
            {
                if (item.transform.position.x < disaplePoint.x)
                    item.SetActive(false);

            }
        }
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
