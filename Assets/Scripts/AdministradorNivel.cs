using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Authors:
 * Aleny Sofía Arévalo Magdaleno
 * Pablo González de la Parra
 * Luis Humberto Romero Pérez
 * Valeria Martínez Silva
 * 
 * Description:
 * This scripts manages the level itself. It starts the background music, the begin and end canvas, the current score
 * of the player and updates the score information canvas automatically
 */

public class AdministradorNivel : MonoBehaviour
{
    public static AdministradorNivel instancia;

    public AudioSource CancionNivel;
    public AudioSource SonidoMultiplicador;


    public int PuntajeActual;
    private int PuntajeGolpeExitoso = 100;
    private int MultiplicadorActual = 1;
    private int ContadorMultiplicador = 0;

    public TextMeshProUGUI TextoPuntajeActual;
    public TextMeshProUGUI TextoMultiplicadorActual;
    public TextMeshProUGUI TextoFinalNivel;

    public bool ComenzarNivel = false;
    public string HoraInicio;
    public string HoraFinal;

    public GameObject CanvasInicioNivel;
    public GameObject CanvasInfoNivel;
    public GameObject BotonSiguienteEscena;

    void Start()
    {
        instancia = this;
        PuntajeActual = 0;
        CanvasInicioNivel.SetActive(true);
        CanvasInfoNivel.SetActive(false);
    }

    void Update()
    {
        //If level hasn't started the function doesn't execute
        if (!ComenzarNivel)
        {
            //If key is pressed, begin level
            if (Input.anyKeyDown)
            {
                empezarNivel();
                HoraInicio = System.DateTime.Now.ToString("HH:mm:ss");
            }
        }
    }

    //Function called when level just began
    public void empezarNivel()
    {
        ComenzarNivel = true;
        CancionNivel.Play();
        CanvasInicioNivel.SetActive(false);
        CanvasInfoNivel.SetActive(true);
    }

    //Function called when level just terminated
    public void TerminarNivel()
    {
        HoraFinal = System.DateTime.Now.ToString("HH:mm:ss");
    }   

    //Function called everytime a note is hit correctly
    public void NotaExitosa()
    {
        //Debug.Log("¡Nota exitosa!");
        PuntajeActual += PuntajeGolpeExitoso * MultiplicadorActual;
        TextoPuntajeActual.text = "Score: " + PuntajeActual;

        ContadorMultiplicador++;

        if (ContadorMultiplicador >= 4)
        {
            MultiplicadorActual++;
            SonidoMultiplicador.Play();
            ContadorMultiplicador = 0;
        }
        else { }
        TextoMultiplicadorActual.text = "Multiplier: x" + MultiplicadorActual;
    }

    //Function called everytime a note wasn't hit on time
    public void NotaFallida()
    {
        //Debug.Log("¡Fallaste!");
        ContadorMultiplicador = 0;
        MultiplicadorActual = 1;
        TextoMultiplicadorActual.text = "Multiplier: x" + MultiplicadorActual;
    }
}
