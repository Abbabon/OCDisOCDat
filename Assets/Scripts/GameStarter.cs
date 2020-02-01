using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Inject] private SoundService _soundService;
    
    void Start()
    {
        Screen.SetResolution(1125, 2436, true);
        _soundService.PlayGameMusic();   
    }
}