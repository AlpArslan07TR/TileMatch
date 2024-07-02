using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [Header("Project Dependency")]
    [SerializeField] Tile tilePrefab;

    [Header("Scene Depencency")]
    [SerializeField] Transform tileParent;

    public Tile[] Tiles { get; set; }

    void Awake()
    {
        TouchEvents.OnElementTapped += TileTapped;

        PrepareTiles();
    }

    void OnDestroy()
    {
        TouchEvents.OnElementTapped -= TileTapped; 
    }

    void PrepareTiles()
    {
        var TileCount = 5;
        Tiles = new Tile[5]; //todo; change with level tile amount 

        for (int i=0; i < TileCount; i++)
        {
            Tiles[i] = Instantiate(tilePrefab, tileParent);
        }
    }
    void TileTapped(ITouchable touchable)
    {
        var tappedTile = touchable.gameObject.GetComponent<Tile>();
    }
}
