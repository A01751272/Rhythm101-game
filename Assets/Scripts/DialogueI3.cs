using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Clase para generar los cuadros de dialogo en el juego
public class DialogueI3 : MonoBehaviour
{
    //Atributos de clase
    public TextMeshProUGUI textComponent; //Texto al que se vinvulara
    public string[] lines; //Lineas de texto a mostrar
    public float textSpeed; //Tiempo de pausa entre letra y letra
    private int index;
    public GameObject ButtonSkip;
    public GameObject Character2;
    public GameObject Character3;

    // Start is called before the first frame update
    void Start()
    {
        //Vaciamos el texto e iniciamos a mostrar dialogo
        textComponent.text = string.Empty;
        StartCoroutine(StartDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        //Revisar si se presiona el click del mouse o enter para adelantar o saltar al sigueinte dialogo
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
        //Iniciamos a mostrar el dialogo
        yield return new WaitForSeconds(1);
        Character2.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
        yield return new WaitForSeconds(5);
        ButtonSkip.SetActive(true);
    }

    IEnumerator TypeLine()
    {
        //Ir escribiendo letra por letra de una linea con la pausa corresponsiente
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        //Revisamos si quedan lineas por mostrar, si si actualizamos la linea actual y la mostramos
        //Si no, dejamos de mostrar la caja de texto
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
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
            Character3.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
