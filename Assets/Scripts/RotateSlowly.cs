using UnityEngine;

public class RotateSlowly : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float _rotationPerSecond = 8f;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.Rotate(0, 0, _rotationPerSecond * Time.deltaTime);
    }
}
