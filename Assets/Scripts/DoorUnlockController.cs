using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLockController : MonoBehaviour
{
    private bool lKeyDown = false;
    public float holdTimerL = 0f;
    public float lockDuration = 3f;
    public GameObject door;
    public Collider2D doorCollider;
    public GameObject doorLockedPanel;
    public float panelDisplayDuration = 5f;

    void Start()
    {
        // Ensure the door and its collider are initially inactive
        door.SetActive(false);
        doorCollider.enabled = false;

        // Ensure the door locked panel is initially inactive
        doorLockedPanel.SetActive(false);
    }

    public void Update()
    {
        // Door Locking Key Handling
        if (Input.GetKeyDown(KeyCode.L))
        {
            lKeyDown = true;
            holdTimerL = 0f;
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            lKeyDown = false;
            holdTimerL = 0f;
        }

        if (lKeyDown)
        {
            holdTimerL += Time.deltaTime;

            if (holdTimerL >= lockDuration)
            {
                LockDoor();
                Debug.Log("Door locked!!");
            }
        }
    }

    void LockDoor()
    {
        door.SetActive(true);
        doorCollider.enabled = true;
        doorLockedPanel.SetActive(true);
        Invoke("DestroyDoorLockedPanel", panelDisplayDuration);
    }

    void DestroyDoorLockedPanel()
    {
        Destroy(doorLockedPanel);
    }
}
