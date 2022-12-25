using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataColor : MonoBehaviour
{

    private SpriteRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(Color color)
    {
        renderer.color = color;
    }
}
