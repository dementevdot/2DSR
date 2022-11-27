using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected int Capacity;

    protected GameObject PoolGameObject;
    protected Camera Camera;

    protected List<GameObject> Pool = new List<GameObject>();

    protected virtual void Init(List<GameObject> prefabs)
    {
        Camera = Camera.main;

        for (int i = 0; i < Capacity; i++)
        {
            GameObject spawned = Instantiate(prefabs[Random.Range(0, prefabs.Count - 1)], PoolGameObject.transform);
            spawned.SetActive(false);

            Pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = Pool.FirstOrDefault(road => road.activeSelf == false);

        return result != null;
    }

    protected void DisableObjectAbroadCamera()
    {
        Vector3 disablePoint = Camera.ViewportToWorldPoint(new Vector2(-0.2f, 0));

        foreach (var item in Pool)
        {
            if (item.activeSelf == true)
            {
                if (item.transform.position.x < disablePoint.x)
                {
                    item.SetActive(false);
                }
            }
        }
    }

    protected List<GameObject> GetActiveObjects()
    {
        List<GameObject> activeObjects = new List<GameObject>();

        foreach (var item in Pool)
            if (item.activeSelf == true)
                activeObjects.Add(item);

        return activeObjects;
    }

    public virtual void ResetPool()
    {
        foreach (var item in Pool)
        {
            item.SetActive(false);
        }
    }
}
