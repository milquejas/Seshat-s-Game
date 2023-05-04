using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSOlist", menuName = "ConversationList")]
public class ConversationListSO : ScriptableObject
{
    public List<ConversationSO> ConversationSO;
}