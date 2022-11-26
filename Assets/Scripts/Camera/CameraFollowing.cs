using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private GameObject _followedGameObject;
    private float _xOffset;

    public void Init(GameObject followedGameObject, float xOffset)
    {
        _followedGameObject = followedGameObject;
        _xOffset = xOffset;
    }

    private void LateUpdate()
    {
        if (_followedGameObject != null)
        {
            transform.position = new Vector3(_followedGameObject.transform.position.x + _xOffset,
                transform.position.y,
                transform.position.z);
        }
        else
        {
            throw new NullReferenceException();
        }
    }
}
