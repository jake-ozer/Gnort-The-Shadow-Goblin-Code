using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public int currentHealth = 300;
    public goblinmovement movement;
    public Animator animator;
    public Rigidbody2D rb;
    public AudioClip deathSound;
    public GameObject deathMenu;
    public gnortshadowball weaponone;
    public gnortshadowblastscript weapontwo;
    public gnortdrawingbow weaponthree;
    private bool isDead = false;
    public GameObject gnortSpell;
    public GameObject gnortGoblin;
    public cinemachineswitcher cameraSwitcher;
    public GameObject shadowTimer;
    public GameObject bossWall1;
    public GameObject bossWall2;
    public healthbarscript healthBar;
    public int maxHealth = 300;
    public GameObject bossBar;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        deathMenu = FindObjectOfType<referencemanager>().deathMenu;
        rb = FindObjectOfType<referencemanager>().gnortRb;
        gnortSpell = FindObjectOfType<referencemanager>().gnortSpell;
        shadowTimer = FindObjectOfType<referencemanager>().shadowTimer;
        movement = FindObjectOfType<referencemanager>().movement;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("You hit a thing");

        if (currentHealth <= 0)
        {
            Die();
        }
        if (gameObject.tag == "mushroom boss")
        {
            healthBar.SetHealth(currentHealth);
        }       
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "gnort")
        {
            gnortSpell.SetActive(false);
            shadowTimer.SetActive(false);
            deathMenu.active = true;
            Time.timeScale = 0f;
            movement.enabled = false;
            animator.SetTrigger("IsDying");
            Debug.Log("You Hit spikes");
            rb.drag = 2000;
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
    }

    void Die()
    {
        if (gameObject.tag == "mushroom boss")
        {
            Debug.Log("Cheese");
            cameraSwitcher.SwitchPriority();
            bossWall1.SetActive(false);
            bossWall2.SetActive(false);
            bossBar.SetActive(false);
            GemCounterScript.gemAmount += 25;
        }
        Destroy(gameObject);
    }
}
