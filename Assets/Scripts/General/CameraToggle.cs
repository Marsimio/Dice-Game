using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera mainCamera;
    public Camera mapCamera;
    public KeyCode toggleKey = KeyCode.M; // Set to keycode for debugging purposes, switch to inputManager later

    private bool mapActive = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            mapActive = !mapActive;
            mainCamera.enabled = !mapActive;
            mapCamera.enabled = mapActive;
        }
    }
}