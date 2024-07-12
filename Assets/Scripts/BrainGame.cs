using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BrainGame : MonoBehaviour
{
    public GameObject brainGamePanel; // The panel for the brain game
    public TMP_Text patternText; // The text to display the pattern
    public TMP_InputField inputField; // The input field for the player to enter the pattern
    public Button submitButton; // The button to submit the player's answer

    public string currentPattern = "5497"; 
    private bool gameActive = false;
    private PointSystem pointSystem;

    void Start()
    {
        brainGamePanel.SetActive(false);
        submitButton.onClick.AddListener(CheckPattern);
        pointSystem = FindObjectOfType<PointSystem>();
    }

    public void StartBrainGame()
    {
        brainGamePanel.SetActive(true);
        patternText.text = "5 4 9 7"; // Display the fixed pattern with spaces
        gameActive = true;
        StartCoroutine(DisplayPattern());
    }

    private IEnumerator DisplayPattern()
    {
        yield return new WaitForSeconds(3); // Time to show the pattern
        patternText.text = "";
        inputField.text = "";
        inputField.gameObject.SetActive(true);
    }

    private void CheckPattern()
    {
        if (!gameActive) return;

        if (inputField.text == currentPattern)
        {
            Debug.Log("Correct Pattern!");
            CompleteTask();
        }
        else
        {
            Debug.Log("Incorrect Pattern. Try Again.");
        }
    }

    private void CompleteTask()
    {
        brainGamePanel.SetActive(false);
        gameActive = false;
        if (pointSystem != null)
        {
            pointSystem.AddPoints(20); // Example of adding points
        }
        else
        {
            Debug.LogError("PointSystem script not found!");
        }
    }
}