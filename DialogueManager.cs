using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public boymovementscript boyMovement;
    private Queue<string> sentences;
    public Animator animator;
    public Text nameText;
    public Text dialogueText;
    public Text notificationText;
    public GameObject textBox;
    public GameObject notificationBox;
    public int time;
    public Text buttonInfoText;
    public GameObject infoText;
    private bool inDialogue = false;
    private boypausescript pauseScript;
    private static bool managerExists;

    void Start()
    {
        sentences = new Queue<string>();
        pauseScript = FindObjectOfType<boypausescript>();

        if (!managerExists)
        {
            managerExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(inDialogue)
        {
            pauseScript.enabled = false;
        }
        else
        {
            pauseScript.enabled = true;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();
        boyMovement.enabled = false;
        inDialogue = true;
        rb.drag = 5000;
        animator.SetBool("InConversation", true);
        textBox.SetActive(true);
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
      string sentence = sentences.Dequeue();
        Debug.Log(sentence);

        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        boyMovement.enabled = true;
        inDialogue = false;
        rb.drag = 0;
        animator.SetBool("InConversation", false);
        textBox.SetActive(false);
    }

    public void DisplayNotification(Dialogue notification)
    {
        StartCoroutine(ShowNotification(notification));
    }

    public IEnumerator ShowNotification(Dialogue notification)
    {
        notificationBox.SetActive(true);
        notificationText.text = notification.name;
        yield return new WaitForSecondsRealtime(time);
        notificationBox.SetActive(false);
    }

    public void DisplayButtonInfo(Dialogue buttonInfo)
    {
        infoText.SetActive(true);
        buttonInfoText.text = buttonInfo.name;
    }
}

