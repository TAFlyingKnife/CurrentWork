using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private bool hidden = true;

    //indexes Chars and next string
    private int index;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideText();
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        /* Starts text if none is present,
         * advances to end of string if one is still running,
         * advances to next string if current is done,
         * and stops if there are no more strings.
         */
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hidden)
            {
                ShowText();
                StartDialogue();
            }
            else if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

        // Skips text really fast like deltarune
        if (Input.GetKey(KeyCode.C))
        {
            if (textComponent.text == lines[index])
            { 
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
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
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            index = 0;
            HideText();
        }
    }

    void HideText()
    {
        textComponent.text = string.Empty;
        gameObject.transform.localScale = Vector3.zero;
        hidden = true;
    }

    void ShowText()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        hidden = false;
    }
}
