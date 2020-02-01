using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoinedUI : MonoBehaviour
{
    [SerializeField]
    private Image[] playerImages = null;
    [SerializeField]
    private Color activeColor = Color.green;
    [SerializeField]
    private float fadeToActiveColorTimer = 0.3f;
    [SerializeField]
    private float fadeOutTimer = 1f;
    [SerializeField]
    private float blinkTime = 0.25f;
    [SerializeField]
    private CanvasGroup playerJoinedCanvasGroup = null;
    
    private Sequence _blinkSequence;
    
    public void SetPlayerActive(int player)
    {
        playerImages[player].DOColor(activeColor, fadeToActiveColorTimer);
    }
    
    private void Awake()
    {
        _blinkSequence = DOTween.Sequence();
        
        GameManager.OnGameStarted += FadeOutCanvasGroup;
    }

    private void Start()
    {
        BlinkCanvasGroup();
    }

    private void BlinkCanvasGroup()
    {
        _blinkSequence.Append(playerJoinedCanvasGroup.DOFade(0, blinkTime));
        _blinkSequence.Append(playerJoinedCanvasGroup.DOFade(1, blinkTime));
        _blinkSequence.SetLoops(-1);
        _blinkSequence.Play();
    }

    private void FadeOutCanvasGroup()
    {
        _blinkSequence.Kill();
        playerJoinedCanvasGroup.DOFade(0, fadeOutTimer);
    }
}
