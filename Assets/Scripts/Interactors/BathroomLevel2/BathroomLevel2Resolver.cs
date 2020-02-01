using System.Linq;
using Boo.Lang;
using UnityEngine;
using Zenject;

public class BathroomLevel2Resolver : LevelResolver
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;

    [SerializeField] private RotatableMedicine[] medicines;
    

    public override void Resolve()
    {
        if (medicines.All(medicine => medicine.Flipped))
        {
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Good3);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.BathroomLevel2));
        }
    }
}