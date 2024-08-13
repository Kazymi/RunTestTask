using System;
using UnityEngine;

[Serializable]
public class PlayerModelConfiguration
{
    [field: SerializeField] public PlayerStateType PlayerStateType { get; private set; }
    [field: SerializeField] public GameObject Model { get; private set; }
}