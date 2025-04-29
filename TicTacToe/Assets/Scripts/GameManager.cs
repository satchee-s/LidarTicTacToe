using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Sprite xSprite;
    [SerializeField] Sprite oSprite;
    [SerializeField] List<TileHandler> tileHandlers = new List<TileHandler>();

    bool isXTurn = true;
    int[] tiles = new int[9]; //1 = x, 0 = o
    int playCount;

    public delegate void OnTileHit(SpriteRenderer sprite, int tileNum);
    public static OnTileHit tileHit;

    private void Start()
    {
        tileHit += ChangeSprite;
        for (int i = 0; i < 9; i++)
        {
            tiles[i] = i + 9;
        }
    }

    void ChangeSprite(SpriteRenderer sprite, int tileNum)
    {
        if (isXTurn)
            sprite.sprite = xSprite;
        else
            sprite.sprite = oSprite;

        tiles[tileNum] = isXTurn ? 1 : 0;
        playCount++;

        GameOver();
        isXTurn = !isXTurn;
    }

    public bool CheckIfWon()
    {
        if (tiles[3] == tiles[4] && tiles[3] == tiles[5])
            return true;
        else if (tiles[1] == tiles[4] && tiles[1] == tiles[7])
            return true;
        else if (tiles[0] == tiles[4] && tiles[8] == tiles[0])
            return true;
        else if (tiles[2] == tiles[4] && tiles[2] == tiles[6])
            return true;
        if (tiles[0] == tiles[1] && tiles[2] == tiles[0])
            return true;
        else if (tiles[0] == tiles[3] && tiles[0] == tiles[6])
            return true;
        if (tiles[6] == tiles[7] && tiles[6] == tiles[8])
            return true;
        else if (tiles[2] == tiles[5] && tiles[2] == tiles[8])
            return true;

        return false;
    }

    void GameOver()
    {
        if (CheckIfWon())
        {
            string winner = isXTurn ? "X" : "O";
            Debug.Log($"Game over! {winner} won!");
            StartCoroutine(StartOver());
        }
        else
            if (playCount >= 9)
        {
            Debug.Log("Game over! It's a tie");
            StartCoroutine(StartOver());
        }
    }

    IEnumerator StartOver()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < tileHandlers.Count; i++)
        {
            tileHandlers[i].CleanUp();
            tiles[i] = i + 9;
        }
        isXTurn = true;
        playCount = 0;
    }
}
