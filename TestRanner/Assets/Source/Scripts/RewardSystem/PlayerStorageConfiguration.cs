using System;
using UnityEngine;

[Serializable]
public class PlayerStorageConfiguration
{
    [field: Range(0f, 1f)]
    [field: SerializeField]
    public float MaxLevel { get; private set; }

    [field: SerializeField] public PlayerStateConfiguration PlayerStateConfiguration { get; private set; }
}