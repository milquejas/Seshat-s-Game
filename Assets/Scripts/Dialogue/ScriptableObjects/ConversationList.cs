using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSOlist", menuName = "Dialogue/ConversationList")]
public class ConversationList : ScriptableObject
{
    public List<Conversation> ConversationSO;
}