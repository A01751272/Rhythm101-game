using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

/* Authors:
 * Aleny Sofía Arévalo Magdaleno
 * Pablo González de la Parra
 * Luis Humberto Romero Pérez
 * Valeria Martínez Silva
 * 
 * Description:
 * This scripts is responsible for storing and sending the initial and final form answers written by the player, creating a POST HTTP request
 * connecting directly to the API to store the information in the database
 */

public class Forms : MonoBehaviour
{
    public TMP_InputField InputFieldAnswer1;
    public TMP_InputField InputFieldAnswer2;
    public TMP_InputField InputFieldAnswer3;
    public TMP_InputField InputFieldAnswer4;
    public TMP_InputField InputFieldAnswer5;
    public TMP_InputField InputFieldAnswer6;
    public TMP_InputField InputFieldAnswer7;
    public TMP_InputField InputFieldAnswer8;

    private int formnumber1 = 1;
    private int formnumber2 = 2;
    private string ability;
    private string opinion;
    private string player;

    public static Forms instancia;

    public GameObject errorpanel;

    //Function called before the Start() function
    void Awake()
    {
        instancia = this;
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            errorpanel.SetActive(false);
        }
    }
    
    //Function called in order to start the coroutine to send initial form information
    public void InsertarDatosFormInicial()
    {
        StartCoroutine(InsertFormInicial());
    }

    //Function called in order to start the coroutine to send final form information
    public void InsertarDatosFormFinal()
    {
        StartCoroutine(InsertFormFinal());
    }

    //Coroutine that sends the initial form information to server
    private IEnumerator InsertFormInicial()
    {
        if(InputFieldAnswer1.text == "" || InputFieldAnswer2.text == "" || InputFieldAnswer3.text == ""
            || InputFieldAnswer4.text == "" || InputFieldAnswer5.text == "" || InputFieldAnswer6.text == ""
            || InputFieldAnswer7.text == "" || InputFieldAnswer8.text == "")
        {
            errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Please fill al inputs before sending the form.";
            errorpanel.SetActive(true);
        } else
        {
            int opinion1 = int.Parse(InputFieldAnswer1.text);
            int opinion2 = int.Parse(InputFieldAnswer2.text);
            int opinion3 = int.Parse(InputFieldAnswer3.text);
            int opinion4 = int.Parse(InputFieldAnswer4.text);
            int ability1 = int.Parse(InputFieldAnswer5.text);
            int ability2 = int.Parse(InputFieldAnswer6.text);
            int ability3 = int.Parse(InputFieldAnswer7.text);
            int ability4 = int.Parse(InputFieldAnswer8.text);

            opinion = ((opinion1 + opinion2 + opinion3 + opinion4) / 4.0).ToString();
            ability = ((ability1 + ability2 + ability3 + ability4) / 4.0).ToString();

            player = PlayerPrefs.GetString("idPlayer", "0"); //GetCurrentIdPlayer
            Debug.Log(player);
            WWWForm Forma = new WWWForm();
            Forma.AddField("number", formnumber1);
            Forma.AddField("ability", ability);
            Forma.AddField("opinion", opinion);
            Forma.AddField("player", player);

            UnityWebRequest postrequest = UnityWebRequest.Post("https://rhythm101-oxy65.ondigitalocean.app/evaluations/", Forma);
            yield return postrequest.SendWebRequest();
            if (postrequest.result == UnityWebRequest.Result.Success)
            {
                string respuestaServidor = postrequest.downloadHandler.text;
                Debug.Log(respuestaServidor);
                Navegacion.Instance.ToLogin();
            }
            else
            {
                errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Error registering form.";
                errorpanel.SetActive(true);
                Debug.Log("Error al registrar form");
            }
        }
    }

    //Coroutine that sends the final form information to server
    private IEnumerator InsertFormFinal()
    {
        if (InputFieldAnswer1.text == "" || InputFieldAnswer2.text == "" || InputFieldAnswer3.text == ""
            || InputFieldAnswer4.text == "" || InputFieldAnswer5.text == "" || InputFieldAnswer6.text == ""
            || InputFieldAnswer7.text == "" || InputFieldAnswer8.text == "")
        {
            errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Please fill al inputs before sending the form.";
            errorpanel.SetActive(true);
        } else
        {
            int opinion1 = int.Parse(InputFieldAnswer1.text);
            int opinion2 = int.Parse(InputFieldAnswer2.text);
            int opinion3 = int.Parse(InputFieldAnswer3.text);
            int opinion4 = int.Parse(InputFieldAnswer4.text);
            int ability1 = int.Parse(InputFieldAnswer5.text);
            int ability2 = int.Parse(InputFieldAnswer6.text);
            int ability3 = int.Parse(InputFieldAnswer7.text);
            int ability4 = int.Parse(InputFieldAnswer8.text);

            opinion = ((opinion1 + opinion2 + opinion3 + opinion4) / 4).ToString();
            ability = ((ability1 + ability2 + ability3 + ability4) / 4).ToString();

            player = PlayerPrefs.GetString("idPlayer", "0"); //CurrentIdPlayer

            WWWForm Forma = new WWWForm();
            Forma.AddField("number", formnumber2);
            Forma.AddField("ability", ability);
            Forma.AddField("opinion", opinion);
            Forma.AddField("player", player);

            UnityWebRequest postrequest = UnityWebRequest.Post("https://rhythm101-oxy65.ondigitalocean.app/evaluations/", Forma);
            yield return postrequest.SendWebRequest();
            if (postrequest.result == UnityWebRequest.Result.Success)
            {
                string respuestaServidor = postrequest.downloadHandler.text;
                Debug.Log(respuestaServidor);
                Navegacion.Instance.ToCredits();
            }
            else
            {
                errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Error registering form.";
                errorpanel.SetActive(true);
                Debug.Log("Error al registrar form 1");
            }
        }
    }
}
