using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSOlist", menuName = "ConversationList")]
public class ConversationList : ScriptableObject
{
    public List<Conversation> ConversationSO;
}