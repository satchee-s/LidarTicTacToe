using System;
using TouchScript.Gestures;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] int tileNumber;

    [Tooltip("IMPORTANT: Assign the SpriteRenderer component from the CHILD GameObject that displays the X/O symbol here.")]
    [SerializeField] SpriteRenderer symbolSpriteRenderer; // This MUST be assigned in the Inspector

    private TapGesture tapGestureComponent; // For TouchScript event handling

    private void Awake()
    {
        // We rely on symbolSpriteRenderer being assigned in the Inspector.
        if (symbolSpriteRenderer == null)
        {
            Debug.LogError($"TileHandler on '{gameObject.name}': SymbolSpriteRenderer is not assigned in the Inspector. Please assign the child's SpriteRenderer.", this);
        }
    }

    // Using OnEnable and OnDisable for event subscription is generally more robust
    // especially if tiles can be enabled/disabled.
    private void OnEnable()
    {
        tapGestureComponent = GetComponent<TapGesture>();
        if (tapGestureComponent != null)
        {
            tapGestureComponent.Tapped += TappedHandler;
        }
        else
        {
            // Only log error if the component is expected to always be there.
            // If TapGesture is optional on some tiles, this might be a warning or handled differently.
            Debug.LogError($"TapGesture component not found on {gameObject.name}. Tap input will not work for this tile.", this);
        }
    }

    private void OnDisable()
    {
        if (tapGestureComponent != null)
        {
            tapGestureComponent.Tapped -= TappedHandler;
        }
    }

    public void CleanUp()
    {
        if (symbolSpriteRenderer != null)
        {
            symbolSpriteRenderer.sprite = null; // Clear the displayed symbol
            // Reset the symbol's visual scale to its default (usually 1,1,1 locally for the child)
            // The pop/flip animations will set the scale again when a symbol is placed.
            symbolSpriteRenderer.transform.localScale = Vector3.one;
        }
    }

    // This public getter is used by GameManager to get the correct SpriteRenderer for animations
    public SpriteRenderer GetSymbolSpriteRenderer()
    {
        return symbolSpriteRenderer;
    }

    private void TappedHandler(object sender, EventArgs e)
    {
        Debug.Log($"TAP DETECTED on GameObject: '{gameObject.name}' with Tile Number: {tileNumber}", this.gameObject);

        if (GameManager.instance != null && GameManager.instance.canPlay)
        {
            if (symbolSpriteRenderer != null)
            {
                // Invoke GameManager's event, passing the SYMBOL'S SpriteRenderer (from the child GameObject)
                GameManager.tileHit?.Invoke(symbolSpriteRenderer, tileNumber);
            }
            else
            {
                // This error would ideally be caught by the Awake check, but good to have defensive checks.
                Debug.LogError($"SymbolSpriteRenderer not assigned or found in TileHandler for {gameObject.name}, cannot process tap.", this);
            }
        }
    }

    // You had two identical GetSpriteRenderer/GetSymbolSpriteRenderer methods; one is enough.
    // I've kept GetSymbolSpriteRenderer as it's more descriptive.
}
