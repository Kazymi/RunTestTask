using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create PlayerConfiguration", fileName = "PlayerConfiguration",
    order = 0)]
public class PlayerConfiguration : ScriptableObject
{
    [field: SerializeField] public float SpeedForward { get; private set; }
    [field: SerializeField] public float SpeedStrafe { get; private set; }
    [field: SerializeField] public float StrafeRotateSpeed { get; private set; }
    [field: SerializeField] public Vector3 RotateStrafe { get; private set; }
}