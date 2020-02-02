using System.Linq;
using UnityEngine;
using Zenject;

public class BathroomLevel2Resolver : LevelResolver
{

    [SerializeField] private RotatableMedicine[] medicines;
    
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

    public override void Resolve()
    {
        if (medicines.All(medicine => medicine.Flipped))
        {
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Good3);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.BathroomLevel2));
        }
    }
}