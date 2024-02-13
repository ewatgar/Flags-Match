using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardBack, cardFlag;
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
        cardBack.gameObject.SetActive(false);
        _bIsCurrentlyFlipped = true;
    }

    public void Unflip()
    {
        cardBack.gameObject.SetActive(true);
        _bIsCurrentlyFlipped = false;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        cardFlag.sprite = image;
        _size = cardBack.sprite.bounds.size;
        Debug.Log("Size memorycard: "+Size);
    }

    void OnMouseDown()
    {
        if (!IsCurrentlyFlipped && controller.CanFlip)
        {
            Flip();
            controller.NotifyCardFlipped(this);
        }
    }
}
