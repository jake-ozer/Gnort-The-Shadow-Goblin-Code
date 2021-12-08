using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinswitchform : MonoBehaviour
{
    public Animator animator;
    public goblinmovement goblinMovement;
    public goblinshadowmovement goblinShadow;
    public Rigidbody2D rb;
    public GameObject gnortGoblin;
    public Collider2D gnortLegs;
    public Collider2D gnortBody;
    public GameObject gnortBow;
    public bool inGround = false;
    public GameObject gnortGround;
    public Collider2D wallCollider;
    public GameObject[] moveThroughable;
    public float shadowModeTimer = 0;
    public bool isCooled = true;
    public float cooldownTimer;
    public Animator animator2;
    public bool isGrounded = false;
    public Transform GroundCheck1; 
    public LayerMask groundLayer; 
    public Transform movingPlatform;
    public bool isOnPlatform;
    public Rigidbody2D rbPlatform;

    // Start is called before the first frame update
    void Start()
    {
        goblinMovement = GetComponent<goblinmovement>();
        goblinShadow = GetComponent<goblinshadowmovement>();
        moveThroughable = GameObject.FindGameObjectsWithTag("Passable");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, groundLayer);

        if ((shadowModeTimer > 2))
        {
            Debug.Log("You stayed in shadow mode too long");
            StopShadow();
            isCooled = false;
            animator2.gameObject.SetActive(false);
        }
        if ((cooldownTimer > 0.1))
        {
            isCooled = true;
        }
        if ((cooldownTimer > 0.09))
        {
            animator2.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.LeftShift) && (isCooled == true))
        {
            GoShadow();
            animator2.SetBool("isShadowing", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopShadow();
            animator2.gameObject.SetActive(false);
            isCooled = false;
        }
        if (isGrounded == true)
        {
            cooldownTimer += Time.deltaTime;
        }
    }
    
    public void GoShadow()
    {
        Debug.Log("You switched Forms");
        animator.SetBool("IsShadow", true);
        goblinMovement.enabled = false;
        goblinShadow.enabled = true;
        rb.gravityScale = 1.5f;
        foreach (GameObject objects in moveThroughable)
        {
            objects.GetComponent<Collider2D>().enabled = false;
        }
        gnortLegs.enabled = false;
        gnortBow.SetActive(false);
        shadowModeTimer += Time.deltaTime;
        cooldownTimer = 0;
    }

    public void StopShadow()
    {
        animator.SetBool("IsShadow", false);
        goblinMovement.enabled = true;
        goblinShadow.enabled = false;
        rb.gravityScale = 2f;
        foreach (GameObject objects in moveThroughable)
        {
            objects.GetComponent<Collider2D>().enabled = true;
        }
        gnortLegs.enabled = true;
        Debug.Log("You switched back");
        gnortBow.SetActive(true);
        
        shadowModeTimer = 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (goblinMovement.enabled == true)
        {

            if (collision.gameObject.tag == "MovingPlatform")
            {
                Debug.Log("you stood on spawned platform");

                movingPlatform = collision.gameObject.transform;
                isOnPlatform = true;

                transform.SetParent(movingPlatform);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            isOnPlatform = false;
            transform.SetParent(null);
        }
    }
}
