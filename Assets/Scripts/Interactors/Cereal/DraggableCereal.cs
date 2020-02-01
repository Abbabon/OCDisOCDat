using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DraggableCereal : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;
    
    [SerializeField] private DragTarget _dragTarget;
    
    protected override void OnDrag()
    {
        if (_dragTarget != null)
        {
            if (_dragTarget.Contains(_transform))
            {
                _transform.localPosition = new Vector3(_dragTarget.transform.localPosition.x, _dragTarget.transform.localPosition.y, _transform.localPosition.z);
                
                ChangeInputState(false);
                _soundService.PlaySoundEffect(SoundService.SoundEffects.Good2);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.CerealLevel2));
            }
            else
            {
                _transform.localPosition = _dragStartPos;
            }
        }
    }

    protected override void OnStartDrag()
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.Slushing);   
    }
}