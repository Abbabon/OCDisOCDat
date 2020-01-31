using System;
using System.Numerics;
using Boo.Lang;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public abstract class Rotatable : MonoBehaviour
{
    private const float TOLERANCE = 5f; 
    private bool _isBeingHeld;
    private bool _inputEnabled = true;

    protected Transform _transform;
    [SerializeField] private float[] RotateTargetValues;

    [Inject] 
    private Camera _mainCamera;

    private Vector3 _screenPos;
    private float _angleOffset;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_inputEnabled && _isBeingHeld)
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                _screenPos = _mainCamera.WorldToScreenPoint (_transform.position);
                var v3 = Input.mousePosition - _screenPos;
                _angleOffset = (Mathf.Atan2(_transform.right.y, _transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;
            }
            if(Input.GetMouseButton(0)) 
            {
                var v3 = Input.mousePosition - _screenPos;
                var angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
                _transform.eulerAngles = new Vector3(0,0,angle+_angleOffset);  
            }

            RotateTargetValues.ForEach(rotateTargetValue =>
            {
                if (Math.Abs(_transform.localRotation.eulerAngles.z - rotateTargetValue) < TOLERANCE)
                {
                    OnRotateTarget(rotateTargetValue);
                }
            });
        }
    }
    
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartRotate();
        }
    }
    
    private void OnMouseUp()
    {
        StopRotate();
    }

    private void StartRotate()
    {
        _isBeingHeld = true;
    }
    private void StopRotate()
    {
        _isBeingHeld = false;
    }

    protected virtual void OnRotateTarget(float rotationAngle)
    {
            
    }
    
    public void ChangeInputState(bool enabled)
    {
        _inputEnabled = enabled;
    }
}