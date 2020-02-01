using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SwappableImage : Draggable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;

    [SerializeField] private SwappableImage _sisterImage;
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
                _sisterImage.MoveToTransform(_sisterImage.DragTarget.transform);
                _sisterImage.ChangeInputState(false);
                
                _soundService.PlaySoundEffect(SoundService.SoundEffects.PictureFlip);
                _soundService.PlaySoundEffect(SoundService.SoundEffects.Good4);
                
                ChangeInputState(false);
                _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.GalleryLevel2));
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