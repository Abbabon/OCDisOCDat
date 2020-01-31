using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TappableShoe : Tappable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private LevelResolver _levelResolver;

    [SerializeField] private Sprite _regularSprite;
    [SerializeField] private Sprite _flippedSprite;

    private SpriteRenderer _spriteRenderer;

    public bool Flipped;

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
        }
        else
        {
            _spriteRenderer.sprite =  _regularSprite;
        }
        
        _levelResolver.Resolve();
    }
}