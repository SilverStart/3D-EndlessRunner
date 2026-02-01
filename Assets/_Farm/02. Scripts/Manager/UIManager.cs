using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        GameScoreManager.scoreChanged += (score) => scoreText.text = score.ToString();
    }
}