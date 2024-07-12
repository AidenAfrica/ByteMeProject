using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class IntroDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public GameObject continueButton;
    public GameObject startGameButton; 

    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(true); 
        dialogueText.text = "";
        continueButton.SetActive(false);
        startGameButton.SetActive(false); 
        StartCoroutine(Typing()); 
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        continueButton.SetActive(false);
        startGameButton.SetActive(false); 
        dialoguePanel.SetActive(false); 
    }

    IEnumerator Typing()
    {
        Debug.Log("Starting Typing coroutine.");
        dialogueText.text = "";
        isTyping = true;
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false;
        Debug.Log("Finished Typing coroutine.");
        continueButton.SetActive(true);
    }

    public void NextLine()
    {
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Typing());
        }
        else
        {
            continueButton.SetActive(false); 
            startGameButton.SetActive(true); 
        }
    }

    // Method to load the game scene
    public void StartGame()
    {
        SceneManager.LoadScene("LevelDesign"); 
    }
}
