using System;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    
    public static event Action<int> scoreChanged;

    private int score = 0;

    public void ScoreUp()
    {
        score += 100;
        scoreChanged?.Invoke(score);
    }
}
