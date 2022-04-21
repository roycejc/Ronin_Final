using System.Collections;
using UnityEngine;
using TMPro;

//namespace DialogueSystem

public class DialogueBase : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] story;
    public float textSpeed;

    private int index;

    private void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            {
            if (textComponent.text == story[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = story[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in story[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    { 
        if(index < story.Length -1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }
}