using UnityEngine;
using Zenject;

public class RotatableMedicine : Rotatable
{
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;
    private LevelResolver _resolver;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, IPromiseTimerService promiseTimerService, SoundService soundService, LevelResolver levelResolver)
    {
        _sceneManagerService = sceneManagerService;
        _promiseTimerService = promiseTimerService;
        _soundService = soundService;
        _resolver = levelResolver;
    }
    
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