using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [Header("Project Dependency")]
    [SerializeField] Tile tilePrefab;
    [SerializeField] LevelSelectionSO levelSelectionSO;

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
        var tileCount = levelSelectionSO.levelData.tiles.Length;
        Tiles = new Tile[tileCount]; 

        for (int i=0; i < tileCount; i++)
        {
            Tiles[i] = Instantiate(tilePrefab, tileParent);
            Tiles[i].Prepare(levelSelectionSO.levelData.tiles[i]);
        }
    }
    void TileTapped(ITouchable touchable)
    {
        var tappedTile = touchable.gameObject.GetComponent<Tile>();
    }
}
