using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] GameObject LevelPanel;
    [SerializeField] Button closeButton;
    [SerializeField] Transform levelsContainer;

    void Awake()
    {
        CloseFast();
    }

    void Appear()
    {
        DOTween.Kill(levelsContainer);

        LevelPanel.SetActive(true);

        levelsContainer.DOScale(1, .28f)
            .OnStart(() => levelsContainer.localScale = Vector3.one * .5f)
            .OnComplete(() => closeButton.interactable = true)
            .SetEase(Ease.OutBack);
    }

    void Disappear()
    {
        DOTween.Kill(levelsContainer);

        levelsContainer.DOScale(0, .28f)
            .OnStart(()=>closeButton.interactable=false)
            .OnComplete(()=>LevelPanel.SetActive(false))
            .SetEase(Ease.OutBack);
    }

    void CloseFast()
    {
        levelsContainer.localScale = Vector3.zero;
        closeButton.interactable = false;
        LevelPanel.SetActive(false);
    }
}
