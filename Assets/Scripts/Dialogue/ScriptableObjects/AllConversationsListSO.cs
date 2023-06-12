using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSOlist", menuName = "Dialogue/ConversationList")]
public class AllConversationsListSO : ScriptableObject
{
    [Header("All conversations can be added here to hop between dialogs through DialogAnswers script")]
    public List<ConversationSO> ConversationSO;
}