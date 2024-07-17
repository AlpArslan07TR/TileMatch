using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour,ITouchable
{
    public SubmitBlock SubmitBlock
    {
        get => _submitBlock;

        set
        {
            if (_submitBlock == value) return;
            _submitBlock = value;
            if (_submitBlock != null)
            {
                _submitBlock.Tile = this;
            }
        }
    }
    
    TileData _tileData;
    private SubmitBlock _submitBlock;
    


    public void Prepare(TileData tileData)
    {
        _tileData = tileData;
        gameObject.name = $"Tile_{_tileData.id}_{ _tileData.character}";

    }
}
