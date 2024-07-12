using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour
{
    public int requiredPoints = 100;
    public TMP_Text pointsText;
    public TMP_Text resultText;

    public int currentPoints = 0;
    private bool gameActive = true;

    void Start()
    {
        UpdatePointsText();
        resultText.text = "";
    }

    public void AddPoints(int points)
    {
        if (gameActive)
        {
            currentPoints += points;
            UpdatePointsText();

            if (currentPoints >= requiredPoints)
            {
                WinGame();
            }
        }
    }

    private void UpdatePointsText()
    {
        pointsText.text = "Points: " + currentPoints.ToString();
    }

    public void WinGame()
    {
        gameActive = false;
        resultText.text = "You Win!";
        Debug.Log("You Win!");
    }

    public void LoseGame()
    {
        gameActive = false;
        resultText.text = "You Lose!";
        Debug.Log("You Lose!");
    }
}