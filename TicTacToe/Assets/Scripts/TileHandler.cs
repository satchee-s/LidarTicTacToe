using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] int tileNumber;
    //SpriteRenderer sprite;
    //bool isHit;
    [Tooltip("Assign the child GameObject's SpriteRenderer that displays X/O symbols.")]
    [SerializeField] SpriteRenderer symbolSpriteRenderer;

    private void Awake()
    {
        symbolSpriteRenderer = GetComponent<SpriteRenderer>();
        if (symbolSpriteRenderer == null)
        {
            Debug.LogError($"TileHandler on GameObject '{gameObject.name}' could not find a SpriteRenderer component attached to it.", this);
        }
    }
    public void CleanUp()
    {
        if (symbolSpriteRenderer != null)
        {
            symbolSpriteRenderer.sprite = null; 
            symbolSpriteRenderer.gameObject.transform.localScale = Vector3.one;
        }
    }

    public SpriteRenderer GetSymbolSpriteRenderer()
    {
        return symbolSpriteRenderer;
    }

    private void Start()
    {
        symbolSpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<TapGesture>().Tapped += TappedHandler;
    }

    private void OnDestroy()
    {
        GetComponent<TapGesture>().Tapped -= TappedHandler;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return symbolSpriteRenderer;
    }
    private void TappedHandler(object sender, EventArgs e) // Or whatever your tap event handler is
    {
        Debug.Log($"TAP DETECTED on GameObject: '{gameObject.name}' with Tile Number: {tileNumber}", this.gameObject);

        if (GameManager.instance != null && GameManager.instance.canPlay)
        {
            if (symbolSpriteRenderer != null)
            {
                // Invoke GameManager's event with the SYMBOL'S SpriteRenderer
                GameManager.tileHit?.Invoke(symbolSpriteRenderer, tileNumber);
            }
            else
            {
                Debug.LogError($"SymbolSpriteRenderer not assigned in TileHandler for {gameObject.name}, cannot process tap.", this);
            }
        }
    }

    // private TapGesture tapGestureComponent;
    // private void OnEnable()
    // {
    //     tapGestureComponent = GetComponent<TapGesture>();
    //     if (tapGestureComponent != null)
    //     {
    //         tapGestureComponent.Tapped += TappedHandler;
    //     } else {
    //          Debug.LogError($"TapGesture component not found on {gameObject.name}", this);
    //     }
    // }
    // private void OnDisable()
    // {
    //     if (tapGestureComponent != null)
    //     {
    //         tapGestureComponent.Tapped -= TappedHandler;
    //     }
    // }
}
