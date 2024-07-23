using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="TileStats",menuName ="ScriptableObjects/TileStats")]
public class TileStatsSO : ScriptableObject
{
    public float executespeed = 50f;
    public Ease executeEase = Ease.OutSine;
}
