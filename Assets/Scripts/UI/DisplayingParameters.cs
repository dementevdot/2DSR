using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisplayingParameters : MonoBehaviour
{
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
}
