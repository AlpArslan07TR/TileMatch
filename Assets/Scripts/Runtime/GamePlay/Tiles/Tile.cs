using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour,ITouchable
{
    [SerializeField] TextMeshPro tmp;
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

        transform.DOLocalMove(submitBlock.transform.position, .5f)
            .SetSpeedBased(true)
            .SetEase(Ease.OutSine);
    }

    public void Undo()
    {

    }

    public string GetCharacter()
    {
        return _tileData.character.ToLowerInvariant();
    }

    public int[] GetChildren()
    {
        if (SubmitBlock != null) return null;

        return _tileData.children;
    }
    public int GetID()
    {
        return _tileData.id;
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
