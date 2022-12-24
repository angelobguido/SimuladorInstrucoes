using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberDisplayController : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    public void ChangeNumber(int number)
    {
        text.text = number.ToString();
    }
    
}
