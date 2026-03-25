using Unity.VisualScripting;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float turnSpeed = 120f;

    private CharacterController controller;

    private State currentState = State.Alive;

    private enum State
    {
        Alive,
        Dead
    }

    public void KillPlayer()
    {
        currentState = State.Dead;
        Debug.Log("Player state is now: " + currentState);

        if (controller != null)
        {
            controller.enabled = false;
        }
        
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        if (currentState == State.Dead)
        {
            if (controller != null)
            {
                controller.enabled = false;
            }
            return;
        }
        
        float moveInput = 0f;
        float turnInput = 0f;

        if (Input.GetKey(KeyCode.W))
            moveInput = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveInput = -1f;

        if (Input.GetKey(KeyCode.A))
            turnInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            turnInput = 1f;

        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        // Rotate player
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);

        // Move player with collision
        Vector3 move = transform.forward * moveInput * moveSpeed;
        controller.Move(move * Time.deltaTime);
    }
}