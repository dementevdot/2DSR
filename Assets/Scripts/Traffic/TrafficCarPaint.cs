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
        if (_isRandomColor)
            _carColor = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        _carPaint.color = _carColor;
    }
}
