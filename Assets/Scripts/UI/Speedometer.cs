using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class Speedometer : DisplayingParameters
{
    private Car _car;
    private TMP_Text _speedometer;

    private void Awake()
    {
        _speedometer = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        _speedometer.text = $"{Mathf.Round(_car.CurrentSpeed * 10)} KM/H";
    }

    public void Init(Car car)
    {
        _car = car;
    }
}
