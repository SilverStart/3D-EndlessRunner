using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    void OnEnable()
    {
        EventBus.scoreChanged += PrintScore;
    }

    void OnDisable()
    {
        EventBus.scoreChanged -= PrintScore;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score++;
            EventBus.OnScoreChanged(score);
        }
    }
    
    private void PrintScore(int score)
    {
        Debug.Log($"현재 점수 : {score}");
    }
}