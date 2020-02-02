using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DraggableLabel : Draggable
{
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;
    
    [SerializeField] private DragTarget _dragTarget;
    
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
                _transform.localPosition = new Vector3(_dragTarget.transform.localPosition.x, _dragTarget.transform.position.y, _transform.localPosition.z);
                
                ChangeInputState(false);
                _soundService.PlaySoundEffect(SoundService.SoundEffects.Good3);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.BathroomLevel1));
            }
            else
            {
                _transform.localPosition = _dragStartPos;
            }
        }
    }

    protected override void OnStartDrag()
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.PillMove);
    }

    protected override void OnEndDrag()
    {
        _soundService.StopSFX();
    }
}