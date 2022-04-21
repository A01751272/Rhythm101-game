using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Este script se encarga de detectar y categorizar cuando cada circulo individual interactua con cada tambor,
 * ya sea en un golpe exitoso o cuando se falla la nota
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
