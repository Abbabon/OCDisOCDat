using System;
using System.Numerics;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public abstract class Draggable : MonoBehaviour
{
    private Vector3 _clickStartPos;
    protected Vector3 _dragStartPos;
    private  bool _isBeingHeld;
    protected  bool _inputEnabled = true;

    protected Transform _transform;

    [SerializeField] private bool limitXAxis;
    [SerializeField] private bool limitYAxis;

    [Inject] 
    private Camera _mainCamera;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_inputEnabled && _isBeingHeld)
        {
            var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var newPos = mousePos - _clickStartPos;
            var localPosition = _transform.localPosition;
            localPosition = new Vector3(limitXAxis ? localPosition.x : newPos.x, 
                                        limitYAxis? localPosition.y : newPos.y, 
                                            localPosition.z);
            _transform.localPosition = localPosition;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
    }

    private void StartDrag()
    {
        _dragStartPos = _transform.localPosition;
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _clickStartPos = mousePos - _transform.localPosition;
        _isBeingHeld = true;
    }
    private void StopDrag()
    {
        _isBeingHeld = false;
        
        OnDrag();
    }

    private void OnMouseUp()
    {
        StopDrag();
    }

    protected virtual void OnDrag()
    {
        
    }

    public void ChangeInputState(bool enabled)
    {
        _inputEnabled = enabled;
    }
}