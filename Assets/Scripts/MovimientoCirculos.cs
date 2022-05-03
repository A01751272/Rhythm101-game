using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

/* Authors:
 * Aleny Sofía Arévalo Magdaleno
 * Pablo González de la Parra
 * Luis Humberto Romero Pérez
 * Valeria Martínez Silva
 * 
 * Description:
 * This script is responsible for starting, stoping and administrating the appearance of the circles, or notes
 * throughout the level, as well as displaying the final score obtained
 */

public class MovimientoCirculos : MonoBehaviour
{

    public float velocidadCirculos = 0;

    //Circles sequence that should appear on screen
    public int[] VectorCirculos = { 0, 1, 2, 3, 0, 1, 3, 0, 1, 2, 1, 3, 0, 1, 3, 0, 2, 1, 2, 0, 3 };

    private bool IniciarCorrutina = false;
    public int MinimumScore;
    public GameObject BotonPausa;


    public static MovimientoCirculos Instance;

    //Function called before the Start() function
    void Awake()
    {
        Instance = this;
    }

    //private string secretKey = "pvah";
    //public string addScoreURL = "http://localhost/pruebaunity/agregaPuntaje.php?";

    public GameObject CanvasFinalNivel;

    void Start()
    {
        Time.timeScale = Navegacion.Instance.isPaused ? 0 : 1;
        CanvasFinalNivel.SetActive(false);
    }

    //Function that displays the circles on screen, and calls the end function when it finishes
    public IEnumerator MostrarCirculos()
    {
        foreach (int i in VectorCirculos)
        {   
            if (i == 4)
            {
                yield return new WaitForSeconds(velocidadCirculos);
            } else {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(velocidadCirculos);
            }
        }
        yield return new WaitForSeconds(2);
        TerminarNivel();
    }

    //Function that is called when level, or the circles are finished
    public void TerminarNivel()
    {
        CanvasFinalNivel.SetActive(true);
        AdministradorNivel.instancia.CanvasInfoNivel.SetActive(false);
        AdministradorNivel.instancia.CancionNivel.Stop();
        if(AdministradorNivel.instancia.PuntajeActual <= MinimumScore)
        {
            AdministradorNivel.instancia.TextoFinalNivel.text = "You didn't reach the minimum score! Try again! Your score was: " + AdministradorNivel.instancia.PuntajeActual;
        }
        else
        {
            AdministradorNivel.instancia.TextoFinalNivel.text = "You finished the level! Congratulations! Your score was: " + AdministradorNivel.instancia.PuntajeActual;
        }
        HidePauseButton();
        AdministradorNivel.instancia.BotonSiguienteEscena.SetActive(true);
        //StartCoroutine(PostScores("1", administrador.instance.PuntajeActual));
        AdministradorNivel.instancia.TerminarNivel();
    }

    public void HidePauseButton()
    {
        BotonPausa.SetActive(false);
    }

    //Function in order to hash password (Not utilized)
    /*
    IEnumerator PostScores(string name, int score)
    {
        string hash = HashInput(name + score + secretKey);
        string post_url = addScoreURL + "name=" +
               UnityWebRequest.EscapeURL(name) + "&score="
               + score + "&hash=" + hash;
        Debug.Log(post_url);
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: "
                    + hs_post.error);
    }
    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue =
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert =
                 BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
    */

    void Update()
    {
        if (AdministradorNivel.instancia.ComenzarNivel)
        {
            if (!IniciarCorrutina)
            {
                StartCoroutine(MostrarCirculos());
                IniciarCorrutina = true;
            }
        }
    }
}
