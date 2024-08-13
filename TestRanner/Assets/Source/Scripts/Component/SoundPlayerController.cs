using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using UnityEngine;

public class SoundPlayerController : MonoBehaviour, IPlaySound
{
    [SerializeField] private SoundPlayConfiguration[] soundPlayConfigurations;
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void Play(SoundPlayType soundPlayType)
    {
        audioSource.PlayOneShot(soundPlayConfigurations.First(t => t.SoundType == soundPlayType).AudioClip);
    }
}

public interface IPlaySound : IGlobalSubscriber
{
    void Play(SoundPlayType soundPlayType);
}

[Serializable]
public class SoundPlayConfiguration
{
    [field: SerializeField] public AudioClip AudioClip { get; private set; }
    [field: SerializeField] public SoundPlayType SoundType { get; private set; }
}

public enum SoundPlayType
{
    Coin,
    Bottle,
    LevelUp,
    Lose,
    Win,
    Boots,
    Door_1,
    Door_2,
    Door_3,
}