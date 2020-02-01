using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SwappableCereal : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;
    
    [SerializeField] private SwappableCereal _sisterCereal;
    [SerializeField] private DragTarget _dragTarget;
    public DragTarget DragTarget => _dragTarget;

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
                _sisterCereal.MoveToTransform(_sisterCereal.DragTarget.transform);
                _sisterCereal.ChangeInputState(false);
                
                _soundService.PlaySoundEffect(SoundService.SoundEffects.Good2);
        
                ChangeInputState(false);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.CerealLevel1));
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

    private void MoveToTransform(Transform targetTransform)
    {
        _transform.localPosition = targetTransform.localPosition;
    }
}