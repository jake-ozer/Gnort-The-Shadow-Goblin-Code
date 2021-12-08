using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boymovementscript : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    public Animator animator;
    public GameObject playerBoy;
    private static bool playerExists;
    private Vector2 moveInput;
    private InputScript inputManager;

    private void Awake()
    {
        inputManager = InputScript.Instance;
        inputManager = FindObjectOfType<InputScript>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //animations and turning
        if (inputManager.GetPlayerMovement() == Vector2.right)
        {
            animator.SetBool("key pressed", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputManager.GetPlayerMovement() == Vector2.left)
        {
            animator.SetBool("key pressed", true);
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (inputManager.GetPlayerMovement() != Vector2.zero)
        {
            animator.SetBool("key pressed", true);
        }
        else
        {
            animator.SetBool("key pressed", false);
        }
    }

    void FixedUpdate()
    {
        //moving the player
        moveInput = inputManager.GetPlayerMovement();
        rb.velocity = moveInput * moveSpeed;
    }
}
