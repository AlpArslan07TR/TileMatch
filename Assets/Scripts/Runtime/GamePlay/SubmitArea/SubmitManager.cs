using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubmitManager : MonoBehaviour
{
    [SerializeField] SubmitBlock[] submitBlocks;
    public SubmitBlock[] SubmitBlocks => submitBlocks;

    public bool HasEmptyBlock()
    {
        return submitBlocks.Count(sb => sb.IsEmpty) > 0;
    }

    public SubmitBlock GetFristEmptyBlock()
    {
        return submitBlocks.FirstOrDefault(sb => sb.IsEmpty);
    }
}
