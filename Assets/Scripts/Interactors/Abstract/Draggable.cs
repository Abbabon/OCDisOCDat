using System;
using System.Numerics;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public abstract class Draggable : MonoBehaviour
{
    private  Vector3 _clickStartPos;
    private  bool _isBeingHeld;
    protected  bool _inputEnabled = true;

    protected Transform _transform;

    [SerializeField] private bool limitXAxis;
    [SerializeField] private bool limitYAxis;

    [Inject] 
    private Camera _mainCamera;

    [Inject] 
    private DragTarget _dragTarget;

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
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _clickStartPos = mousePos - _transform.localPosition;
        _isBeingHeld = true;
    }
    private void StopDrag()
    {
        _isBeingHeld = false;
        
        //TODO: any callback that needs to be performed when releasing the object? Check if the position is inside the position
        if (_dragTarget != null)
        {
            if (_dragTarget.Contains(_transform))
            {
                OnDrag(_dragTarget.DragTransform);
            }
        }
    }

    private void OnMouseUp()
    {
        StopDrag();
    }

    protected virtual void OnDrag(Transform dragTransform)
    {
        
    }

    public void ChangeInputState(bool enabled)
    {
        _inputEnabled = enabled;
    }
}