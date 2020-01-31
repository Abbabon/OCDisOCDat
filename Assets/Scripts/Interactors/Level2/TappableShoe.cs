using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TappableShoe : Tappable
{
    [Inject] private SceneManagerService _sceneManagerService;
    [Inject] private LevelResolver _levelResolver;
    private SpriteRenderer _spriteRenderer;

    private bool flipped;
    public bool Flipped => flipped;

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
        if (!flipped)
        {
            _spriteRenderer.color = Color.cyan;
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
        
        flipped = !flipped;
        _levelResolver.Resolve();
    }
}