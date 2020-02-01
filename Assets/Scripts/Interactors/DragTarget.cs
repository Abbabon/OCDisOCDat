using System;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private Transform _dragTransform;
    public Transform DragTransform => _dragTransform;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _dragTransform = GetComponent<Transform>();
    }

    public bool Contains(Transform targetTransform, bool local = true)
    {
        return _boxCollider2D.bounds.Contains(new Vector3(local ? targetTransform.localPosition.x : targetTransform.position.x, 
                                                                local ? targetTransform.localPosition.y : targetTransform.position.y, 
                                                                _dragTransform.localPosition.z));
    }
}