
using System;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    
    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public bool Contains(Transform targetTransform)
    {
        return _boxCollider2D.bounds.Contains(targetTransform.localPosition);
    }
}