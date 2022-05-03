using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is responsible for detecting and categorizing when each individual circle, or note, interacts with
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

    public void GolpeCorrecto()
    {
        AdministradorNivel.instancia.NotaExitosa();
        FuePresionado = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TerminarConGolpe()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);
        FuePresionado = false;
    }

    public void TerminarSinGolpe()
    {
        AdministradorNivel.instancia.NotaFallida();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.SetActive(false);
        FuePresionado = false;
    }

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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            RangoPresionar = false;
        }
    }
}
