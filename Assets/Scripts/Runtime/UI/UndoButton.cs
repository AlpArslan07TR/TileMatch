using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Board board;


    private void Awake()
    {
        button.onClick.AddListener(OnClick);
        
    }
    private void Start()
    {
        ObserveCommand();
    }
    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void ObserveCommand()
    {
        gameObject.ObserveEveryValueChanged(_ => board.TileCommandInvoker.HasCommand())
            .Subscribe(hasCommand =>
            {
                button.interactable = hasCommand;
            }).AddTo(gameObject);
    }


    private void OnClick()
    {
        if (!DOTween.IsTweening(transform))
        {
            transform.DOPunchScale(Vector3.one * .1f, .3f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => transform.localScale = Vector3.one);
        }
        board.TileCommandInvoker.RemoveCommand();
    }
}
