using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PointSystem : MonoBehaviour
{
    public int requiredPoints = 100;
    public TMP_Text pointsText;
    public TMP_Text resultText;

    public int currentPoints = 0;
    private bool gameActive = true;
    private Timer timer;

    void Start()
    {
        UpdatePointsText();
        resultText.text = "";
        timer = FindObjectOfType<Timer>();
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
        if (gameActive)
        {
            gameActive = false;
            resultText.text = "You Win!";
            Debug.Log("You Win!");

            if (timer != null)
            {
                timer.StopTimer();
            }

            // Load win screen here
            SceneManager.LoadScene("WinScene");
        }
    }

    public void LoseGame()
    {
        if (gameActive)
        {
            gameActive = false;
            resultText.text = "You Lose!";
            Debug.Log("You Lose!");

            // Load lose screen here
            SceneManager.LoadScene("LoseScene");

        }
    }
}
