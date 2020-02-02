using Zenject;

public class Level2Resolver : LevelResolver
{
    private TappableShoe _shoe;
    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;
    
    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, IPromiseTimerService promiseTimerService, SoundService soundService, TappableShoe shoe)
    {
        _sceneManagerService = sceneManagerService;
        _promiseTimerService = promiseTimerService;
        _soundService = soundService;
        _shoe = shoe;
    }
    
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