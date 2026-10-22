using UnityEngine;
using UnityEngine.UI;

public class shields_counter : MonoBehaviour
{
    private Text scoreText;
    private Text rewardText;
    private int currentPuntuation = 0;
    private int rewardThreshold = 100;

    void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("score-ui");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<Text>();
            if (scoreText != null) 
            {
                scoreText.text = "";
            }
        }
        GameObject rewardObject = GameObject.FindGameObjectWithTag("reward-ui");
        if (rewardObject != null)
        {
            rewardText = rewardObject.GetComponent<Text>();
            if (rewardText != null)
            {
                rewardText.text = "";
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("low-value-shield"))
        {
            AddPoints(5);
        }
        else if (other.CompareTag("high-value-shield"))
        {
            AddPoints(10);
        }
    }

    private void AddPoints(int points)
    {
        currentPuntuation += points;
        CheckReward();
        UpdateScoreUI();
    }

    private void CheckReward()
    {
        if (currentPuntuation >= rewardThreshold)
        {
            rewardThreshold += 100;
            if (rewardText != null)
            {
                rewardText.text = "¡Reward obtained!";
            }
        } 
        else
        {
            {
                rewardText.text = "";
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentPuntuation;
        }
    }
}
