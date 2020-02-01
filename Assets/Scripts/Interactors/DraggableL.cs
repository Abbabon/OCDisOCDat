using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DraggableL : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    
    [Inject] private SoundService _soundService;
    
    [SerializeField] private DragTarget _dragTarget;

    [SerializeField] private Sprite _placedSprite;
    
    
    protected override void OnDrag()
    {
        if (_dragTarget != null)
        {
            if (_dragTarget.Contains(_transform, false))
            {
                _soundService.PlaySoundEffect(SoundService.SoundEffects.StartButton);
                
                _transform.position = new Vector3(_dragTarget.transform.position.x, _dragTarget.transform.position.y, _transform.localPosition.z);

                _spriteRenderer.sprite = _placedSprite;

                ChangeInputState(false);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.GameStarter));
            }
            else
            {
                _transform.localPosition = _dragStartPos;
            }
        }
    }
}