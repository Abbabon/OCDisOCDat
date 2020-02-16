using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SwappableCar : Draggable
{
    [SerializeField] private SwappableCar _sisterCar;
    [SerializeField] private DragTarget _dragTarget;
    public DragTarget DragTarget => _dragTarget;

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
                _transform.localPosition = new Vector3(_dragTarget.transform.localPosition.x, 
                                                        _dragTarget.transform.localPosition.y, 
                                                        _transform.localPosition.z);

                //swap locations
                _sisterCar.MoveToTransform(_sisterCar.DragTarget.transform);
                _sisterCar.ChangeInputState(false);
        
                _soundService.PlaySoundEffect(SoundService.SoundEffects.CarSwap);
                _soundService.PlaySoundEffect(SoundService.SoundEffects.Good5);
                
                ChangeInputState(false);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.CarsLevel2));
            }
            else
            {
                _transform.localPosition = _dragStartPos;
            }
        }
    }

    private void MoveToTransform(Transform targetTransform)
    {
        if (_transform != null)
        {
            _transform.localPosition = targetTransform.localPosition;
        }
    }
}