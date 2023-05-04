using UnityEngine;

[CreateAssetMenu(fileName = "New character dialog", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public string CharacterName;
    public Sprite PortraitImage;
}
