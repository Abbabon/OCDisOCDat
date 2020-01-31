using UnityEngine;

public class RotatableSquare : Rotatable
{
    private bool _locked = false;
    
    protected override void OnRotateTarget()
    {
        base.OnRotateTarget();

        if (!_locked)
        {
            _locked = true;
            Debug.Log("heeeerrre");
        }
    }
}