using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappableFinder : MonoBehaviour
{
    [SerializeField] Board board;

    private void Awake()
    {
        GameEvents.OnSearchVisibleTiles += Search;
    }

    private void OnDestroy()
    {
        GameEvents.OnSearchVisibleTiles -= Search;
    }
    private void Search()
    {
        
        foreach (var tile in board.Tiles)
        {
            tile.UpdateVisual(board.IsVisible(tile));
        }
    }
}
