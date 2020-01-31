using UnityEngine;

public class RotatableSquare : Rotatable
{
    private bool _locked = false;
    
    protected override void OnRotateTarget(float rotationAngle)
    {
        _transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        ChangeInputState(false);
        
        if (!_locked)
        {
            _locked = true;
            Debug.Log("heeeerrre");
        }
    }
}