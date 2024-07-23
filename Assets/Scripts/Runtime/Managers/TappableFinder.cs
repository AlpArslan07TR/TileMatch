using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappableFinder : MonoBehaviour
{
    [SerializeField] Board board;


    private void Update()
    {
        foreach (var tile in board.Tiles)
        {
            tile.UpdateVisual(board.IsVisible(tile));
        }
    }
}
