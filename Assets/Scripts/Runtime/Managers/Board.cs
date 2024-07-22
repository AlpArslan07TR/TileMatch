using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [Header("Project Dependency")]
    [SerializeField] Tile tilePrefab;
    [SerializeField] LevelSelectionSO levelSelectionSO;

    [Header("Scene Depencency")]
    [SerializeField] Transform tileParent;
    [SerializeField] SubmitManager submitManager;

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
        var tileCount = levelSelectionSO.levelData.tiles.Length;
        Tiles = new Tile[tileCount]; 

        for (int i=0; i < tileCount; i++)
        {
            Tiles[i] = Instantiate(tilePrefab, tileParent);
            Tiles[i].Prepare(levelSelectionSO.levelData.tiles[i]);
        }

        GameEvents.OnTilesSpawned?.Invoke(Tiles);
    }
    void TileTapped(ITouchable touchable)
    {
        var tappedTile = touchable.gameObject.GetComponent<Tile>();

        if (!canTap(tappedTile)) return;
        if (!submitManager.HasEmptyBlock()) return;

        bool canTap(Tile tile)
        {
            return tile.SubmitBlock == null
                && IsVisible(tile);
        }

        bool IsVisible(Tile tile)
        {
            return Tiles.All(t => t.GetChildren() == null 
            || Array.IndexOf(t.GetChildren(), tile.GetID()) == -1);
        }
    }
}
