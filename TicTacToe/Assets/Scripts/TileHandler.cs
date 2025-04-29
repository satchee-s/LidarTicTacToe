using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] int tileNumber;
    SpriteRenderer sprite;
    bool isHit;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (!isHit)
        {
            GameManager.tileHit?.Invoke(sprite, tileNumber);
            isHit = true;
        }
    }

    public void CleanUp()
    {
        sprite.sprite = null;
        isHit = false;
    }
}
