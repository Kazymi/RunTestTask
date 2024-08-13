using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerRewardUI
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image slider;

    private const float ColorTimer = 0.6f;
    private const float ValueTimer = 0.3f;

    public void UpdateSlider(float value, Color color)
    {
        slider.DOKill();
        slider.DOColor(color, ColorTimer);
        slider.DOFillAmount(value, ValueTimer);
    }

    public void SetCanvasStatus(bool value)
    {
        canvas.enabled = value;
    }

    public void SetAllValue(float valueSlider, string description, Color color)
    {
        slider.fillAmount = valueSlider;
        slider.color = color;
        this.description.text = description;
        this.description.color = color;
    }

    public void UpdateDescription(string text, Color color)
    {
        description.transform.DOKill();
        description.DOKill();
        description.DOColor(color, ColorTimer);
        var durationShake = 0.3f;
        var strengthShake = 0.3f;
        description.transform.DOShakeScale(durationShake, strengthShake)
            .OnComplete(() => description.transform.localScale = Vector3.one);
        description.text = text;
    }
}