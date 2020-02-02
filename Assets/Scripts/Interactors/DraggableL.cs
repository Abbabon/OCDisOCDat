using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DraggableL : Draggable
{
    [SerializeField] private DragTarget _dragTarget;
    [SerializeField] private Sprite _placedSprite;

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
            if (_dragTarget.Contains(_transform, false))
            {
                _soundService.PlaySoundEffect(SoundService.SoundEffects.StartButton);
                
                _transform.position = new Vector3(_dragTarget.transform.position.x, _dragTarget.transform.position.y, _transform.localPosition.z);

                _spriteRenderer.sprite = _placedSprite;

                ChangeInputState(false);
                _promiseTimerService.WaitFor(1f).Then(() =>
                {
                    _sceneManagerService.UnloadScene(ScenesEnum.GameStart);
                    _sceneManagerService.LoadScene(ScenesEnum.SlippersLevel1);
                });
            }
            else
            {
                _transform.localPosition = _dragStartPos;
            }
        }
    }
}