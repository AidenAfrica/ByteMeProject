using UnityEngine;
using System.Collections.Generic;

public class SabotageCode : MonoBehaviour
{
    [System.Serializable]
    public class PrefabInfo
    {
        public string type; // Identifier for the type of prefab
        public GameObject prefab; // Reference to the prefab
        public Collider2D disableCollider; // Collider to disable when this prefab is collected
    }

    [System.Serializable]
    public class DropOffZone
    {
        public string zoneName; // Name or identifier for the drop-off zone (for debugging purposes)
        public Transform transform; // Reference to the drop-off zone's transform
        public bool isEnabled = true; // Flag to enable/disable this drop-off zone
    }

    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] List<PrefabInfo> prefabList; // List of different prefab types
    [SerializeField] Transform playerTransform; // Reference to the player's transform
    [SerializeField] List<DropOffZone> dropOffZones; // List of drop-off zones

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

                    // Disable specific collider if provided in PrefabInfo
                    if (prefabInfo.disableCollider != null)
                    {
                        prefabInfo.disableCollider.enabled = false;
                    }

                    // Example: assuming pointSystem has a method to update points
                    //if (pointSystem != null)
                    //{
                    //    pointSystem.AddPoints(10); // Example: Add points when object is picked up
                    //}

                    return; // Exit loop once a matching type is found
                }
            }
        }

        if (hasPackage && other.CompareTag("Sabotaged"))
        {
            Debug.Log("You have sabotaged the object!");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;

            // Find an available drop-off zone
            foreach (var dropOffZone in dropOffZones)
            {
                if (dropOffZone.isEnabled)
                {
                    Debug.Log("Dropping off at: " + dropOffZone.zoneName);

                    // Move collectedPrefab to drop-off zone and instantiate it there
                    if (currentCollectedObject != null)
                    {
                        GameObject copyObject = Instantiate(currentCollectedObject, dropOffZone.transform.position, Quaternion.identity);
                        Destroy(currentCollectedObject); // Destroy the original instance with the player
                        currentCollectedObject = null; // Clear reference
                    }

                    // Check if it's a brain game sabotage
                    if (brainGame != null)
                    {
                        brainGame.StartBrainGame();
                    }
                    else
                    {
                        Debug.LogError("BrainGame script not found!");
                    }

                    return; // Exit loop once a drop-off zone is found
                }
            }

            Debug.LogWarning("No available drop-off zone found!");
        }
    }
}