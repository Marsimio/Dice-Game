using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera mainCamera;
    public Camera mapCamera;

    private bool mapActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))  // Set to keycode for debugging purposes, switch to inputManager later
        {
            mapActive = !mapActive;
            mainCamera.enabled = !mapActive;
            mapCamera.enabled = mapActive;
        }
    }
}