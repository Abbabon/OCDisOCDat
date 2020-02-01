using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _gameMusic;

    private Dictionary<SoundEffects, AudioClip> _sfx;

    private void Awake()
    {
        _sfx = new Dictionary<SoundEffects, AudioClip>();
        LoadSoundsFromResources();
    }

    private void LoadSoundsFromResources()
    {
        foreach (var soundEffectEnum in Enum.GetValues(typeof(SoundEffects)))
        {
            var resource = Resources.Load<AudioClip>($"{soundEffectEnum.ToString()}");
            if (resource != null)
            {
                _sfx.Add((SoundEffects) soundEffectEnum, resource);
                Debug.Log($"Loaded {soundEffectEnum.ToString()}");
            }
        }
    }
    public void PlayGameMusic()
    {
        _musicSource.clip = _gameMusic;
        _musicSource.Play();
    }
    
    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public enum SoundEffects
    {
        StartButton,
        DraggingSlipper,
        FlipUp,
        FlipDown,
        PillMove,
        BottleFlipStart,
        BottleFlipEnd,
        ClostDoorClose,
        CarSwap,
        CarCrash,
        Slushing,
        SpoonDrag,
        PictureFlip,
        PictureSwing,
        Worm,
        Disgusting,
        Good1,
        Good2,
        Good3,
        Good4,
        Good5,
        Bad1,
        Bad2,
        Bad3,
        Bad4
    }

    public void PlaySoundEffect(SoundEffects sfx)
    {
        if (_sfx.ContainsKey(sfx))
            _sfxSource.PlayOneShot(_sfx[sfx]);
    }

    public void StopSFX()
    {
        _sfxSource.Stop();
    }
}