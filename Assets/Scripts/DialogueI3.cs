using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/* Authors:
 * Aleny Sofía Arévalo Magdaleno
 * Pablo González de la Parra
 * Luis Humberto Romero Pérez
 * Valeria Martínez Silva
 * 
 * Description: 
 * This scripts shows the dialogue of the characters inside the third interlude scene
 */

public class DialogueI3 : MonoBehaviour
{
    //Class atributes
    public TextMeshProUGUI textComponent; //Linked text
    public string[] lines; //Lines of text to be shown
    public float textSpeed; //Time paused between words
    private int index;
    public GameObject ButtonSkip;
    public GameObject Character2;
    public GameObject Character3;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //Dump the text and begin the dialogue
        textComponent.text = string.Empty;
        textSpeed = 0.04f;
        index = 0;
        StartCoroutine(StartDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        //Check whether the mouse is clicked in order to advance to next script
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator StartDialogue()
    {
        //Begin to show dialogue
        yield return new WaitForSeconds(1);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //Writing line per line in the screen
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        //Check if there are any unfinished shown lines
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            if(index == 1)
            {
                Character2.SetActive(true);
            }
            if (index == 2)
            {
                ButtonSkip.SetActive(true);
            }
            if (index == 4)
            {
                Character2.SetActive(false);
                Character3.SetActive(true);
            }
            StartCoroutine(TypeLine());
        }
        else
        {
            StopAllCoroutines();
            Character3.SetActive(false);
            gameObject.SetActive(false);
            SceneManager.LoadScene("LevelThree");
        }
    }
}
