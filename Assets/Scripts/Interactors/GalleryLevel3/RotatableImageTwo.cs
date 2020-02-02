using UnityEngine;
using Zenject;

//create a target scene in a common class
public class RotatableImageTwo : Rotatable
{
    [SerializeField] private GameObject _worm;

    private bool _locked = false;
    
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
    
    protected override void OnRotateTarget(float rotationAngle)
    {
        _transform.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        ChangeInputState(false);
        
        if (!_locked)
        {
            _locked = true;
            
            _worm.SetActive(true);
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Worm);
            
            _promiseTimerService.WaitFor(2.5f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.GalleryLevel3));
        }
    }
}