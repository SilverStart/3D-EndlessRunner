using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 90f;

    private GameScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = FindFirstObjectByType<GameScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.ScoreUp();
            Destroy(gameObject);
        }
    }
}