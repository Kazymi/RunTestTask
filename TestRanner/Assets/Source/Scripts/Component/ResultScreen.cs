using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour, IToResultScreen
{
    [SerializeField] private Canvas resultCanvas;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text buttonText;

    [SerializeField] private Image fadeImage;
    [SerializeField] private Color colorWin;
    [SerializeField] private Color colorLose;

    [SerializeField] private Image resultImage;
    [SerializeField] private Sprite lose;
    [SerializeField] private Sprite win;

    [SerializeField] private Button restartGameButton;

    [SerializeField] private Transform resultScreenPanel;
    [SerializeField] private Transform resultStartPosition;
    [SerializeField] private Transform resultEndPosition;

    private void Awake()
    {
        restartGameButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void Finish(bool isWin)
    {
        resultCanvas.enabled = true;
        var floatDuration = 0.7f;
        buttonText.text = isWin ? "Next level" : "Try again";
        resultText.text = isWin ? "Level Complete" : "Ooops!";
        resultScreenPanel.transform.position = resultStartPosition.position;
        resultScreenPanel.transform.DOMove(resultEndPosition.position, floatDuration);
        fadeImage.DOColor(isWin ? colorWin : colorLose, floatDuration);
    }
}

public interface IToResultScreen : IGlobalSubscriber
{
    void Finish(bool isWin);
}