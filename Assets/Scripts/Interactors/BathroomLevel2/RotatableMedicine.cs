using UnityEngine;
using Zenject;

public class RotatableMedicine : Rotatable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private LevelResolver _resolver;
    [Inject] private SoundService _soundService;
    
    private bool _locked = false;
    public bool Flipped = false;
    
    protected override void OnRotateTarget(float rotationAngle)
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.BottleFlipEnd);
        
        _transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        ChangeInputState(false);
        
        if (!_locked)
        {
            _locked = true;
            Flipped = true;
            _resolver.Resolve();
        }
    }

    protected override void OnStartRotate()
    {
        _soundService.PlaySoundEffect(SoundService.SoundEffects.BottleFlipStart);
    }
}