using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TappableClostDoor : Tappable
{

    [SerializeField] private GameObject _closedDoor;
    private SpriteRenderer _spriteRenderer;
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
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnTap()
    {
        if (!_locked)
        {
            _locked = false;
            _spriteRenderer.enabled = false;
            _closedDoor.SetActive(true);
            
            _soundService.PlaySoundEffect(SoundService.SoundEffects.ClostDoorClose);
            _soundService.PlaySoundEffect(SoundService.SoundEffects.Bad3);
            
            _promiseTimerService.WaitFor(1f).Then(() => _sceneManagerService.UnloadSceneAndLoadNext(ScenesEnum.BathroomLevel3));
        }
    }
}