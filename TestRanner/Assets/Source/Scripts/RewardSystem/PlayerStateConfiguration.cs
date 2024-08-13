using System;
using UnityEngine;

[Serializable]
public class PlayerStateConfiguration
{
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public PlayerStateType PlayerState { get; private set; }
    [field: SerializeField] public float AnimationMoveFloatValue { get; private set; }
}