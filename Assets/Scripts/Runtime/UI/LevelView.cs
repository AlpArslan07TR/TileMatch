using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] GameObject LevelPanel;
    [SerializeField] Button closeButton;
    [SerializeField] Transform levelsContainer;

    void Appear()
    {
        LevelPanel.SetActive(true);

        levelsContainer.DOScale(1, .28f)
            .OnStart(() => levelsContainer.localScale = Vector3.one * .5f)
            .OnComplete(() => closeButton.interactable = true)
            .SetEase(Ease.OutBack);
    }

    void Disappear()
    {

    }
}
