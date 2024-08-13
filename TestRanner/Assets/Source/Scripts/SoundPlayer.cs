using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundPlayType _soundPlayType;

    public void Play()
    {
        EventBus.RaiseEvent<IPlaySound>(t => t.Play(_soundPlayType));
    }
}