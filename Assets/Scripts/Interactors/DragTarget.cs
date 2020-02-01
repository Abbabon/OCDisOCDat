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

    public bool Contains(Transform targetTransform)
    {
        return _boxCollider2D.bounds.Contains(new Vector3(targetTransform.localPosition.x, targetTransform.localPosition.y, _dragTransform.localPosition.z));
    }
}