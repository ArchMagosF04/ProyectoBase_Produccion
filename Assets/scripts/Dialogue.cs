using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;//el text area sirve pra espesificar primero el minimo a maximo de lineas de dialog a mostra

    private bool isPlaterInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime= 0.05f;
    private Movement script;//referencia al moviemiento del jugador, que es tomado cuando el jugador colisiona con el personajes que tiene dialogo.

    void Update()
    {
        if (isPlaterInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])//pasa a la siguiente linea de dialogo
            {
                NextDialogueLine();
            }
            else // si apreto fire 1 mientras se esta mostrando las lineas de texto, muestro todas las que faltan
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue() 
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
        script.StartDialogue = true;
    }

    private void NextDialogueLine() 
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else 
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            script.StartDialogue = false;
        }
    }

    private IEnumerator ShowLine()//utilizamos una corutina para mostar la linea de dialogo caracter por caracter(efecto de tipeo).
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex]) 
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//cheque si el jugador esta a rango para iniciar el dialogo
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            isPlaterInRange = true;
            dialogueMark.SetActive(true);
            script = collision.GetComponent<Movement>();//cargamos la referencia del movimiento al script para impedir que el jugador se mueva.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlaterInRange = false;
            dialogueMark.SetActive(false);
        }
    }
}
