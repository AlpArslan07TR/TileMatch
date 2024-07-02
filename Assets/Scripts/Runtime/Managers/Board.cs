using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Project Dependency")]
    [SerializeField] Tile tilePrefab;

    [Header("Scene Depencency")]
    [SerializeField] Transform tileParent;

    public Tile[] Tiles { get; set; }

    void PrepareTiles()
    {
        var TileCount = 5;
        Tiles = new Tile[5]; //todo; change with level tile amount 

        for (int i=0; i < TileCount; i++)
        {
            Tiles[i] = Instantiate(tilePrefab, tileParent);
        }
    }
}
