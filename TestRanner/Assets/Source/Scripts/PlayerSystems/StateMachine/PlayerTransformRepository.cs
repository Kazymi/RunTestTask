using System;
using UnityEngine;

[Serializable]
public class PlayerTransformRepository
{
    [field: SerializeField] public Transform PlayerMain { get; private set; }
    [field: SerializeField] public Transform PlayerStrafeRotateObject { get; private set; }
}