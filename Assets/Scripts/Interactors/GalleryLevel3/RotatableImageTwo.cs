using UnityEngine;
using Zenject;

//create a target scene in a common class
public class RotatableImageTwo : Rotatable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;

    [SerializeField] private GameObject _worm;

    private bool _locked = false;
    
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