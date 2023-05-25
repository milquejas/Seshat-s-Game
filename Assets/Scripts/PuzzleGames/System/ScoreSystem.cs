using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int maxScore;

    public int UpdateScore(int playerScore)
    {
        return Mathf.Min(playerScore + 1, maxScore);
    }
}
