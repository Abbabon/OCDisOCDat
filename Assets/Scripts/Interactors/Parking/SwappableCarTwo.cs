using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SwappableCarTwo : Draggable
{
    [SerializeField] private SwappableCarTwo _sisterCar;
    [SerializeField] private DragTarget _dragTarget;
    public DragTarget DragTarget => _dragTarget;
    
    [SerializeField] private GameObject _crashObject;
    
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, IPromiseTimerService promiseTimerService, SoundService soundService)
    {
        _sceneManagerService = sceneManagerService;
        _promiseTimerService = promiseTimerService;
        _soundService = soundService;
    }
    
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
        
                _soundService.PlaySoundEffect(SoundService.SoundEffects.CarCrash);
                _soundService.StopMusic();
                
                ChangeInputState(false);
                _sisterCar.ChangeInputState(false);
                _promiseTimerService.WaitFor(2f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.CarsLevel3));
            }
        }
    }

    protected override void OnEndDrag()
    {
        if (_inputEnabled)
        {
            _transform.localPosition = _dragStartPos;
        }
    }

    private void MoveToTransform(Transform targetTransform)
    {
        _transform.localPosition = targetTransform.localPosition;
    }
}