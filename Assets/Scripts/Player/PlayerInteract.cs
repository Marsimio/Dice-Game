using System;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    public float InteractRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Change to input manager
        {
            Ray r = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
