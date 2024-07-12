using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageCode : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;

    public bool hasPackage;
    private SpriteRenderer spriteRenderer;
    private PointSystem pointSystem;
    private BrainGame brainGame;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointSystem = FindObjectOfType<PointSystem>();
        brainGame = FindObjectOfType<BrainGame>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("We have sabotaged the object!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sabotage" && !hasPackage)
        {
            Debug.Log("Sabotage Object picked up!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }

        if (other.tag == "Sabotaged" && hasPackage)
        {
            Debug.Log("You have sabotaged the object!");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;

            // Check if it's a brain game sabotage
            if (brainGame != null)
            {
                brainGame.StartBrainGame();
            }
            else
            {
                Debug.LogError("BrainGame script not found!");
            }
        }
    }
}