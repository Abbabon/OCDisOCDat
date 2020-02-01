using System.Linq;
using Boo.Lang;
using UnityEngine;
using Zenject;

public class Level2Resolver : LevelResolver
{
    [Inject] private TappableShoe _shoe;
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;

    public override void Resolve()
    {
        if (!_shoe.Flipped)
        {
            _shoe.ChangeInputState(false);
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Good1);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.SlippersLevel2));
        }
    }
}