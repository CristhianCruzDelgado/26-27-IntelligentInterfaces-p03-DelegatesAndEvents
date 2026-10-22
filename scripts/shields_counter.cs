using UnityEngine;
using UnityEngine.UI;

public class shields_counter : MonoBehaviour
{
    private Text scoreText;
    private Text rewardText;
    private int currentPuntuation = 0;
    private int nextRewardThreshold = 100;

    private void Start()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("score-ui");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<Text>();
            scoreText.text = "";
        }
        GameObject rewardObject = GameObject.FindGameObjectWithTag("reward-ui");
        if (rewardObject != null)
        {
            rewardText = rewardObject.GetComponent<Text>();
            rewardText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shield-low-value"))
        {
            AddPoints(5);
        }
        else if (other.CompareTag("shield-high-value"))
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
        if (currentPuntuation >= nextRewardThreshold)
        {
            nextRewardThreshold += 100;
            if (rewardText != null)
            {
                rewardText.text = "¡Recompensa obtenida!";
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
