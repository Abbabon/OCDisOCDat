using System;
using UnityEngine;
using Zenject;

public class PlayStartSound : MonoBehaviour
{
    [SerializeField] private SoundService.SoundEffects _soundEffect;
    private SoundService _soundService;
    
    [Inject]
    private void Initialize(SoundService soundService)
    {
        _soundService = soundService;
    }
    
    private void Start()
    {
        _soundService.PlaySoundEffect(_soundEffect);
    }
}