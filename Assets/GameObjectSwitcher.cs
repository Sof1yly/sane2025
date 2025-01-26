using System.Collections;
using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
    public GameObject[] objects; // Array of GameObjects to switch between
    public float autoSwitchTime = 3f; // Time in seconds between automatic switches

    private int currentObjectIndex = 0;
    private float timer;

    void Start()
    {
        if (objects.Length > 0)
        {
            ActivateObject(currentObjectIndex);
        }
        timer = autoSwitchTime;
    }

    void Update()
    {
        // Automatic switching logic
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SwitchToNextObject();
            timer = autoSwitchTime;
        }

        // Raycast logic to detect if the pointer is over the object
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == objects[currentObjectIndex])
                {
                    SwitchToNextObject();
                }
            }
        }
    }

    private void SwitchToNextObject()
    {
        if (objects.Length == 0) return;

        // Disable the current object
        objects[currentObjectIndex].SetActive(false);

        // Move to the next object
        currentObjectIndex = (currentObjectIndex + 1) % objects.Length;

        // Enable the new object
        ActivateObject(currentObjectIndex);
    }

    private void ActivateObject(int index)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == index);
        }
    }
}
