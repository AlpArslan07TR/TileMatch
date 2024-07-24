using System.Linq;
using UnityEngine;

public class SubmitManager : MonoBehaviour
{
    [SerializeField] SubmitBlock[] submitBlocks;
    public SubmitBlock[] SubmitBlocks => submitBlocks;

    private void Awake()
    {
        GameEvents.OnTileAttached += OnWordAttachedCallBack;
        GameEvents.OnTileRemoved += OnWordRemovedCallBack;
    }

    private void OnDestroy()
    {
        GameEvents.OnTileAttached -= OnWordAttachedCallBack;
        GameEvents.OnTileRemoved -= OnWordRemovedCallBack;
    }
    public bool HasEmptyBlock()
    {
        return submitBlocks.Count(sb => sb.IsEmpty) > 0;
    }

    public SubmitBlock GetFirstEmptyBlock()
    {
        return submitBlocks.FirstOrDefault(sb => sb.IsEmpty);
    }

    private void OnWordAttachedCallBack(SubmitBlock submitBlock,string word)
    {
        var nonEmptyBlocks = submitBlocks.Where(sb=> !sb.IsEmpty);
        var combinedWord = string.Join("", nonEmptyBlocks.Select(sb => sb.Character));
        //todo:word manager set current word
    }

    private void OnWordRemovedCallBack(SubmitBlock submitBlock)
    {
        var nonEmptyBlocks = submitBlocks.Where(sb => !sb.IsEmpty);
        var combinedWord = string.Join("", nonEmptyBlocks.Select(sb => sb.Character));
        
    }

    private void OnWordSubmittedCallBack()
    {
        foreach(var block in submitBlocks)
        {
            if (!block.IsEmpty)
            {
                //todo:clear
            }
        }
    }
}

