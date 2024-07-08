using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelSaveData
{
    public LevelScoreData[] Data;

    public LevelSaveData(LevelScoreData[]data)
    {
        Data = data;
    }
}
