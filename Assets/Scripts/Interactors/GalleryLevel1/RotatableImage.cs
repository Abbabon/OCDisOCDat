using UnityEngine;
using Zenject;

public class RotatableImage : Rotatable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;
    
    private bool _locked = false;
    
    protected override void OnRotateTarget(float rotationAngle)
    {
        _transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        ChangeInputState(false);

        if (!_locked)
        {
            _locked = true;
            _soundService.PlaySoundEffect(SoundService.SoundEffects.PictureSwing);
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Good4);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.GalleryLevel1));
        }
    }

    protected override void OnStartRotate()
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.PictureSwing);
    }
}