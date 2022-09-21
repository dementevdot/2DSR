using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInput : MonoBehaviour
{
    private int _verticalInput;

    public event UnityAction<Vector3> InputUpdated;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            _verticalInput = 1;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            _verticalInput = -1;
        else
            _verticalInput = 0;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0 || _verticalInput != 0)
            InputUpdated?.Invoke(new Vector3(horizontalInput, 0, _verticalInput));
    }
}
