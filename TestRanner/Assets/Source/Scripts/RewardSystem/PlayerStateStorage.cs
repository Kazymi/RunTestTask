using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create PlayerStateStorage", fileName = "PlayerStateStorage",
    order = 0)]
public class PlayerStateStorage : ScriptableObject
{
    [field: SerializeField] public PlayerStorageConfiguration[] PlayerStorageConfigurations { get; private set; }
}