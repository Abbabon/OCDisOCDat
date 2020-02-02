using System.Linq;
using Zenject;

public class Level3Resolver : LevelResolver
{
    private TappableShoe[] _shoes;
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, IPromiseTimerService promiseTimerService, SoundService soundService, TappableShoe[] shoes)
    {
        _sceneManagerService = sceneManagerService;
        _promiseTimerService = promiseTimerService;
        _soundService = soundService;
        _shoes = shoes;
    }
    
    public override void Resolve()
    {
        if (_shoes.All(shoe => shoe.Flipped))
        {
            for (int i = 0; i < _shoes.Length; i++)
            {
                _shoes[i].ChangeInputState(_shoes[i]);
            }
            
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Bad1);
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.SlipperLevel3));
        }
    }
}