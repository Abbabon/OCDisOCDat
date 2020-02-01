using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SwappableCarTwo : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;

    [SerializeField] private SwappableCarTwo _sisterCar;
    [SerializeField] private DragTarget _dragTarget;

    public DragTarget DragTarget => _dragTarget;

    [SerializeField] private GameObject _crashObject;
    
    protected override void OnDrag()
    {
        if (_dragTarget != null)
        {
            if (_dragTarget.Contains(_transform))
            {
                // if (_checkContinuously)
                //     _transform.localPosition = new Vector3(_dragTarget.transform.localPosition.x, _dragTarget.transform.localPosition.y, _transform.localPosition.z);

                //swap locations
                _sisterCar.SpriteRenderer.enabled = false;
                _spriteRenderer.enabled = false;
                _crashObject.SetActive(true);
        
                ChangeInputState(false);
                _sisterCar.ChangeInputState(false);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.CarsLevel3));
            }
            else
            {
                _transform.localPosition = _dragStartPos;
            }
        }
    }

    private void MoveToTransform(Transform targetTransform)
    {
        _transform.localPosition = targetTransform.localPosition;
    }
}