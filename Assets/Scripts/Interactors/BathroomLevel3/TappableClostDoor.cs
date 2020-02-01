using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TappableClostDoor : Tappable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private IPromiseTimerService _promiseTimerService;
    [Inject] private SoundService _soundService;
    
    [SerializeField] private GameObject _closedDoor;
    private SpriteRenderer _spriteRenderer;
    private bool _locked = false;
    
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