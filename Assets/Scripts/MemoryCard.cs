using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer CardBack, CardFlag;
    public Controller controller;
    private bool _bIsCurrentlyFlipped = false;
    private int _id;
    private Vector2 _size;

    public bool IsCurrentlyFlipped
    {
        get { return _bIsCurrentlyFlipped; }
    }

    public int Id
    {
        get { return _id; }
    }

    public Vector2 Size
    {
        get { return _size; }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Flip()
    {
        CardBack.gameObject.SetActive(false);
        _bIsCurrentlyFlipped = true;
    }

    public void Unflip()
    {
        CardBack.gameObject.SetActive(true);
        _bIsCurrentlyFlipped = false;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        CardFlag.sprite = image;
        _size = CardBack.sprite.bounds.size;
    }

    void OnMouseDown()
    {
        if (!IsCurrentlyFlipped)
        {
            Flip();
        }
    }
}
