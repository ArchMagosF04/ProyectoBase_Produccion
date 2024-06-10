using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> speakerName;
    private Queue<string> sentences;

    public bool hasEndEvent;
    public UnityEvent OnEndDialogue = new UnityEvent();

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
        ScreenManager.Instance.isDialogueActive = true;

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
        ScreenManager.Instance.isDialogueActive = false;
        animator.SetBool("IsOpen", false);
        Time.timeScale = 1;

        if(hasEndEvent)
        {
            OnEndDialogue?.Invoke();
            hasEndEvent = false;
        }
    }
}
