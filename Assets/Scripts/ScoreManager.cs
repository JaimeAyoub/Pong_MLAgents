using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int scorePlayer1;

    public int scorePlayer2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(GameObject player)
    {
        if (player.CompareTag("Player"))
        {
            scorePlayer1++;
            Debug.Log("AddScore to player 1");
        }
        else if (player.CompareTag("Player2"))
        {
            scorePlayer2++;
            Debug.Log("AddScore to player 2");
        }
        else
        {
            Debug.Log("Nadie toco la bola");
        }
    }
}