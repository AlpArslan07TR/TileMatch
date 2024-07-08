using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CompleteData
{
    public int Score;
    public int Index;

    public CompleteData(int index, int score)
    {
        Index = index;
        Score = score;
    }
}
