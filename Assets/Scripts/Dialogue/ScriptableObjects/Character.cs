using UnityEngine;

[CreateAssetMenu(fileName = "New character dialog", menuName = "Character")]
public class Character : ScriptableObject
{
    public string CharacterName;
    public Sprite PortraitImage;
}