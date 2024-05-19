using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> speakerName;
    private Queue<string> sentences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        speakerName = new Queue<string>();
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if(animator.GetBool("IsOpen")==true &&Input.GetKeyDown(KeyCode.E)) 
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogues dialogue)
    {
        animator.SetBool("IsOpen", true);

        Time.timeScale = 0;

        //nameText.text= dialogue.speakerName;

        speakerName.Clear();
        sentences.Clear();

        foreach (string name in dialogue.speakerName)
        {
            speakerName.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string name=speakerName.Dequeue();
        string sentence=sentences.Dequeue();

        nameText.text = name;
        //dialogueText.text = sentence;

        StopAllCoroutines();  //En caso de querer que aparesca una letra a la vez.
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Time.timeScale = 1;
    }
}
