using UnityEngine;

[CreateAssetMenu(fileName = "New character dialog", menuName = "Dialogue/Character")]
public class CharacterSO : ScriptableObject
{
    public string CharacterName;
    public Sprite PortraitImage;
}
