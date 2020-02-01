using System.Linq;
using Boo.Lang;
using RSG;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

public class Level3Resolver : LevelResolver
{
    [Inject] private TappableShoe[] _shoes;
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;
    
    public override void Resolve()
    {
        if (_shoes.All(shoe => shoe.Flipped))
        {
            //TODO: end sequence

            _shoes.ForEach(shoe => shoe.ChangeInputState(false));
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Bad1);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.SlipperLevel3));
        }
    }
}