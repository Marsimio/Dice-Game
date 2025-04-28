using System.Numerics;
using Unity.Hierarchy;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMotors : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float gravity = -9.8f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * (StatsManager.instance.speed * Time.deltaTime));
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public void Jump()
    {
        Debug.Log("Attempting to jump. isGrounded: " + isGrounded);
        
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(StatsManager.instance.jumpHeight * -3.0f * gravity);
        }
    }
}
