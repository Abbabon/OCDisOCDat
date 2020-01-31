using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tappable : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTap();
        }
    }

    protected void OnTap()
    {
        
    }
}
