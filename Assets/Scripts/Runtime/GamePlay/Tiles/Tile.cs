using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour,ITouchable,ITileCommand
{
    [SerializeField] TileStatsSO tileStats;
    [SerializeField] TextMeshPro tmp;
    [SerializeField] SpriteRenderer spriteRenderer;

    public bool IsClear { get; private set; }
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
    Vector3 _basePos;
    private SubmitBlock _submitBlock;
    


    public void Prepare(TileData tileData)
    {
        _tileData = tileData;
        gameObject.name = $"Tile_{_tileData.id}_{ _tileData.character}";
        SetPosition(_tileData.position);
        SetCharacterText(_tileData.character);

    }

    public void Execute(SubmitBlock submitBlock)
    {
        SubmitBlock = submitBlock;

        DOTween.Kill(transform);

        transform.DOMove(submitBlock.transform.position,tileStats.executespeed)
            .SetSpeedBased(true)
            .SetEase(tileStats.executeEase);

        AudioManager.Instance.PlaySound("Swoosh");
        GameEvents.OnSearchVisibleTiles?.Invoke();
    }

    public void Undo()
    {
        

        DOTween.Kill(transform);
        if(SubmitBlock !=null)
        {
            SubmitBlock.Tile = null;
            SubmitBlock=null;
        }

        AudioManager.Instance.PlaySound("Swoosh");
        transform.DOMove(_basePos, tileStats.executespeed * 2)
            .SetSpeedBased(true)
            .SetEase(tileStats.executeEase);

        
        GameEvents.OnSearchVisibleTiles?.Invoke();
    }

    

    public string GetCharacter()
    {
        return _tileData.character.ToLowerInvariant();
    }

    public int[] GetChildren()
    {
        if (SubmitBlock != null || IsClear) return null;

        return _tileData.children;
    }

    public void Clear()
    {
        IsClear = true;
        SubmitBlock.Tile = null;
        SubmitBlock = null;

        transform.DOScale(0, .28f).SetEase(Ease.InBack);
    }

    public int GetID()
    {
        return _tileData.id;
    }

    public void UpdateVisual(bool isVisible)
    {
        spriteRenderer.color = isVisible ? Color.white : Color.gray;
    }

    private void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
        _basePos = transform.position;
    }

    private void SetCharacterText(string character)
    {
        tmp.text = character;
    }
}
