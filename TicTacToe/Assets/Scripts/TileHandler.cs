using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] int tileNumber;
    SpriteRenderer sprite;
    bool isHit;

    public void CleanUp()
    {
        sprite.sprite = null;
        isHit = false;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    private void OnDestroy()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }
    private void tappedHandler(object sender, EventArgs e)
    {
        Debug.Log("tapped...");
        if (!isHit)
        {
            GameManager.tileHit?.Invoke(sprite, tileNumber);
            isHit = true;
        }
    }
}
