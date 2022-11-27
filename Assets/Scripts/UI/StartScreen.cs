using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : Screen
{
    public event Action StartButtonClick;

    public override void Close()
    {
        gameObject.SetActive(false);
        Button.interactable = false;
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Button.interactable = true;
    }

    protected override void OnButtonClick()
    {
        StartButtonClick.Invoke();
    }
}
