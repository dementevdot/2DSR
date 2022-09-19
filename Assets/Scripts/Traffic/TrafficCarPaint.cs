using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCarPaint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _carPaint;
    [SerializeField] private Color _carColor;
    [SerializeField] private bool _isRandomColor;

    private void OnEnable()
    {
        float minColor = 0;
        float maxColor = 1;

        if (_isRandomColor)
            _carColor = new Color(Random.Range(minColor,maxColor), Random.Range(minColor, maxColor), Random.Range(minColor, maxColor), maxColor);

        _carPaint.color = _carColor;
    }
}
