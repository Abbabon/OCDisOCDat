using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TappableShoe : Tappable
{
    [SerializeField] private Sprite _regularSprite;
    [SerializeField] private Sprite _flippedSprite;

    private SpriteRenderer _spriteRenderer;

    public bool Flipped;

    private SceneManagerService _sceneManagerService;
    private IPromiseTimerService _promiseTimerService;
    private SoundService _soundService;
    private LevelResolver _levelResolver;

    [Inject]
    private void Initialize(SceneManagerService sceneManagerService, IPromiseTimerService promiseTimerService, SoundService soundService, LevelResolver resolver)
    {
        _sceneManagerService = sceneManagerService;
        _promiseTimerService = promiseTimerService;
        _soundService = soundService;
        _levelResolver = resolver;
    }
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnTap()
    {
        Flip();
    }

    private void Flip()
    {
        Flipped = !Flipped;
        
        if (Flipped)
        {
            _spriteRenderer.sprite = _flippedSprite;
            _soundService.PlaySoundEffect(SoundService.SoundEffects.FlipUp);
        }
        else
        {
            _spriteRenderer.sprite =  _regularSprite;
            _soundService.PlaySoundEffect(SoundService.SoundEffects.FlipDown);
        }
        
        _levelResolver.Resolve();
    }
}