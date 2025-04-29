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

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }
    private void tappedHandler(object sender, EventArgs e)
    {
        var gesture = sender as TapGesture;
        HitData hit = gesture.GetScreenPositionHitData();
        if (!isHit)
        {
            GameManager.tileHit?.Invoke(sprite, tileNumber);
            isHit = true;
        }
    }
}
