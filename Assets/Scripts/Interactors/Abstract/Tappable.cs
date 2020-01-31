using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tappable : MonoBehaviour
{
    private bool _inputEnabled = true;
    
    private void OnMouseDown()
    {
        if (_inputEnabled && Input.GetMouseButtonDown(0))
        {
            OnTap();
        }
    }

    protected virtual void OnTap()
    {
        
    }
    
    public void ChangeInputState(bool enabled)
    {
        _inputEnabled = enabled;
    }
}
