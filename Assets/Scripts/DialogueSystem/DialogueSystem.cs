using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public string[] lines;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject textBox;
    private float speedText = 0.06f;
    private int index;

    private void Start()
    {
        dialogueText.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index >= lines.Length)
            {
                Debug.LogWarning($"Index is out of range in SkipText. Index {index}, length {lines.Length}");
                return;
            }
            SkipText();
        }
    }

    public void StartDialogue()
    {        
        if(lines != null)
        {
            textBox.SetActive(true);
            if (lines.Length > 0)
            {
                index = 0;
                dialogueText.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                Debug.LogWarning("No lines to display in dialogue!");
            }
        }
    }

    private IEnumerator TypeLine()
    {
        if (index < lines.Length)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(speedText);
            }
        }
        else
        {
            Debug.LogError("Index out of range in TypeLine()");
        }
    }

    private void SkipText()
    {
        

        if (dialogueText.text == lines[index])
        {
            dialogueText.text = string.Empty;
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = lines[index];
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textBox.SetActive(false);
            lines = null;
        }
    }
}
