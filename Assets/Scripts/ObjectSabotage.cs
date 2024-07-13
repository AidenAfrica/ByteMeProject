using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageCode : MonoBehaviour
{
    [System.Serializable]
    public class PrefabInfo
    {
        public string type; // Identifier for the type of prefab
        public GameObject prefab; // Reference to the prefab
    }

    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] List<PrefabInfo> prefabList; // List of different prefab types
    [SerializeField] Transform playerTransform; // Reference to the player's transform
    [SerializeField] Transform dropOffZoneTransform; // Reference to the drop-off zone's transform

    private bool hasPackage;
    private SpriteRenderer spriteRenderer;
    private PointSystem pointSystem;
    private BrainGame brainGame;
    private GameObject currentCollectedObject; // Track the currently collected object instance

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointSystem = FindObjectOfType<PointSystem>(); // Assuming PointSystem is a MonoBehaviour in the scene
        brainGame = FindObjectOfType<BrainGame>(); // Assuming BrainGame is a MonoBehaviour in the scene
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("We have sabotaged the object!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPackage)
        {
            foreach (var prefabInfo in prefabList)
            {
                if (other.CompareTag(prefabInfo.type))
                {
                    Debug.Log("Sabotage Object picked up: " + prefabInfo.type);
                    hasPackage = true;
                    spriteRenderer.color = hasPackageColor;

                    // Instantiate the correct prefab on the player
                    currentCollectedObject = Instantiate(prefabInfo.prefab, playerTransform);
                    currentCollectedObject.transform.localPosition = Vector3.zero; // Set position relative to player
                    currentCollectedObject.transform.SetParent(playerTransform); // Set parent to player

                    //Destroy(other.gameObject, destroyDelay); // Destroy the trigger object

                    // Example: assuming pointSystem has a method to update points
                    //if (pointSystem != null)
                    //{
                       // pointSystem.AddPoints(10); // Example: Add points when object is picked up
                    //}

                    return; // Exit loop once a matching type is found
                }
            }
        }

        if (hasPackage && other.tag == "Sabotaged")
        {
            Debug.Log("You have sabotaged the object!");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;

            if (currentCollectedObject != null)
            {
                GameObject copyObject = Instantiate (currentCollectedObject, dropOffZoneTransform.position, Quaternion.identity);
                copyObject.transform.SetParent(null);
                Destroy(currentCollectedObject);
            }

            // Move collectedPrefab to drop-off zone and instantiate it there
            currentCollectedObject.transform.SetParent(null); // Unparent from player
            currentCollectedObject.transform.position = dropOffZoneTransform.position; // Move to drop-off zone
            currentCollectedObject = null; // Clear reference

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

