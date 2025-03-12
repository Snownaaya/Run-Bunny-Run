using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CharacterConfig", fileName = "CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [field: SerializeField] public RunningStateConfig RunningConfig { get; private set; }
    [field: SerializeField] public AirbornStateConfig AirbornConfig { get; private set; }
}