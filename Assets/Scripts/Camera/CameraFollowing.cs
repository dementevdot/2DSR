using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private GameObject _followedGameObject;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        if (_followedGameObject != null)
        {
            transform.position = new Vector3(_followedGameObject.transform.position.x + _xOffset,
                transform.position.y,
                transform.position.z);
        }
    }
}
