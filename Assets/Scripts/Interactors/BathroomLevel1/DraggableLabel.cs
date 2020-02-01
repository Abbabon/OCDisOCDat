using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DraggableLabel : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;
    
    [SerializeField] private DragTarget _dragTarget;
    
    protected override void OnDrag()
    {
        if (_dragTarget != null)
        {
            if (_dragTarget.Contains(_transform, false))
            {
                _transform.position = new Vector3(_dragTarget.transform.position.x, _dragTarget.transform.position.y, _transform.localPosition.z);
                
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