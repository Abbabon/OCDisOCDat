using System;
using UnityEngine;
using Zenject;

public class PlayStartSound : MonoBehaviour
{
    [Inject] private SoundService _soundService;

    [SerializeField] private SoundService.SoundEffects _soundEffect;

    private void Start()
    {
        _soundService.PlaySoundEffect(_soundEffect);
    }
}