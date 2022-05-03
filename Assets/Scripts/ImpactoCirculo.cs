using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Authors:
 * Aleny Sofía Arévalo Magdaleno
 * Pablo González de la Parra
 * Luis Humberto Romero Pérez
 * Valeria Martínez Silva
 * 
 * Description:
 * This script is responsible for detecting and categorizing when each individual circle, or note, interacts with
 * a specific drum, wether it was a succesfull hit or a missed note
 */

public class ImpactoCirculo : MonoBehaviour
{
    private bool RangoPresionar;
    private bool FuePresionado;

    public KeyCode TeclaCorrecta;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(TeclaCorrecta))
        {
            if (RangoPresionar)
            {
                GolpeCorrecto();
            }
        }
    }

    //Function called when a note is hit on time
    public void GolpeCorrecto()
    {
        AdministradorNivel.instancia.NotaExitosa();
        FuePresionado = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //Function called when the note ends with a proper hit
    public void TerminarConGolpe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);
        FuePresionado = false;
    }

    //Function called when the note ends without a proper hit
    public void TerminarSinGolpe()
    {
        AdministradorNivel.instancia.NotaFallida();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);
        FuePresionado = false;
    }

    //Function that detects when circle enters the drum, and inside drum colliders
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            RangoPresionar = true;
        }

        if (other.tag == "Finish")
        {
            if (FuePresionado)
            {
                TerminarConGolpe();
            }
            else
            {
                TerminarSinGolpe();
            }
        }
    }

    //Function that detects when circle exits the drum, and inside drum colliders
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            RangoPresionar = false;
        }
    }
}
