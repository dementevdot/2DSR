using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInput : MonoBehaviour
{
    public event UnityAction<Vector3> InputUpdated;

    private int verticalInput;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            verticalInput = 1;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            verticalInput = -1;
        else
            verticalInput = 0;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0 || verticalInput != 0)
            InputUpdated?.Invoke(new Vector3(horizontalInput, 0, verticalInput));
    }
}
