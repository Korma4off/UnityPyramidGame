using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{ 
    [SerializeField] private int _position;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public int position => _position;

    [SerializeField] private Color _color;
    public Color color => _color;

    public void SetColor(Color color)
    {
        _color = color;
        _spriteRenderer.color = color;
    }
}
