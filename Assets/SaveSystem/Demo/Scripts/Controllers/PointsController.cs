using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    
    public float Points
    {
        get => _points;
        set => _points = value;
    }
    
    private float _points = 0;

    private void Update()
    {
        _points += Time.deltaTime;
        text.text = $"Points: {_points.ToString("F0")}";
    }
}
