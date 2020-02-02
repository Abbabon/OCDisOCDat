using System;
using System.Numerics;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public abstract class Draggable : MonoBehaviour
{
    private Vector3 _clickStartPos;
    protected Vector3 _dragStartPos;
    private bool _isBeingHeld;
    protected bool _inputEnabled = true;

    [SerializeField] protected bool _checkContinuously;

    protected Transform _transform;

    [SerializeField] private bool limitXAxis;
    [SerializeField] private bool limitYAxis;

    protected SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private Camera _mainCamera;
    
    [Inject]
    private void Initialize(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

            if (_checkContinuously)
            {
                OnDrag();
            }
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
        OnStartDrag();
        _dragStartPos = _transform.localPosition;
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _clickStartPos = mousePos - _transform.localPosition;
        _isBeingHeld = true;
    }
    private void StopDrag()
    {
        _isBeingHeld = false;
        OnEndDrag();
        
        if (!_checkContinuously)
            OnDrag();
    }

    private void OnMouseUp()
    {
        StopDrag();
    }

    protected virtual void OnDrag()
    {
        
    }
    
    protected virtual void OnStartDrag()
    {
        
    }
    
    protected virtual void OnEndDrag()
    {
        
    }

    public void ChangeInputState(bool enabled)
    {
        _inputEnabled = enabled;
    }
}