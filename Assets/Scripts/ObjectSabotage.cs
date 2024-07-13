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
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform dropOffZoneTransform;



    public bool hasPackage;
    private SpriteRenderer spriteRenderer;
    private PointSystem pointSystem;
    private BrainGame brainGame;
    private GameObject currentCollectedObject; // Track the currently collected object instance


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
        if (!hasPackage)
        {
            foreach (var prefabInfo in prefabList)
            {
                if (other.CompareTag(prefabInfo.type))
                {
                    Debug.Log("Sabotage Object picked up!" + prefabInfo.type);
                    hasPackage = true;
                    spriteRenderer.color = hasPackageColor;

                    currentCollectedObject = Instantiate(prefabInfo.prefab, playerTransform);
                    currentCollectedObject.transform.localPosition = Vector3.zero; // Set position relative to player
                    currentCollectedObject.transform.SetParent(playerTransform); // Set parent to player

                    Destroy(other.gameObject, destroyDelay); // Destroy the trigger object

                }

                if (hasPackage && other.tag == "Sabotaged")
                {
                    Debug.Log("You have sabotaged the object!");
                    hasPackage = false;
                    spriteRenderer.color = noPackageColor;

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
    }
}