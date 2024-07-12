using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timer = 30f;
    public TMP_Text timerText;
    private bool timerActive = false;
    public PointSystem pointSystem;

    void Start()
    {
        UpdateTimerText();
        Debug.Log("start is called");
        pointSystem = FindObjectOfType<PointSystem>();
    }

    public void Update()
    {
        if (timerActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timer = 0;
                timerActive = false;
                UpdateTimerText();
                CheckGameOver();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            timerActive = true;
            Debug.Log("Player entered enemy zone, timer started.");
        }
    }

    public void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void CheckGameOver()
    {
        if (pointSystem != null)
        {
            if (pointSystem.currentPoints >= pointSystem.requiredPoints)
            {
                pointSystem.WinGame();
            }
            else
            {
                pointSystem.LoseGame();
            }
        }
        else
        {
            Debug.LogError("PointSystem script not found!");
        }
    }
}