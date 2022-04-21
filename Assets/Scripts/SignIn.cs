using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SignIn : MonoBehaviour
{
    public TMP_InputField InputFieldName;
    public TMP_InputField InputFieldBirthday;
    public TMP_InputField InputFieldCountry;
    public TMP_InputField InputFieldCity;
    public TMP_InputField InputFieldUsername;
    public TMP_InputField InputFieldPassword;

    private string URLValidarJugador;
    private string URLRegistroJugador;
    public string idPlayerForm;
    public SignIn Instance;
    void Awake()
    {
        Instance = this;
    }

    public void InsertarDatosRegistro()
    {
        StartCoroutine(InsertNewPlayer());
    }

    private IEnumerator InsertNewPlayer()
    {
        if (InputFieldName.text == "" || InputFieldBirthday.text == "" || InputFieldCountry.text == "" || InputFieldCity.text == "" || InputFieldUsername.text == "" || InputFieldPassword.text == "")
        {
            Debug.Log("Please fill al inputs before signing in");
        }
        else
        {
            string username = InputFieldUsername.text;

            URLValidarJugador = "http://localhost:3000/players/" + username;
            UnityWebRequest request = UnityWebRequest.Get(URLValidarJugador);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Ya existe este username");
            }
            else
            {
                string name = InputFieldName.text;
                string birthday = InputFieldBirthday.text;
                string country = InputFieldCountry.text;
                string city = InputFieldCity.text;
                string password = InputFieldPassword.text;

                WWWForm Forma = new WWWForm();
                Forma.AddField("name", name);
                Forma.AddField("birthday", birthday);
                Forma.AddField("country", country);
                Forma.AddField("city", city);
                Forma.AddField("username", username);
                Forma.AddField("password", password);

                UnityWebRequest postrequest = UnityWebRequest.Post("https://rhythm101-oxy65.ondigitalocean.app/players/", Forma);
                yield return postrequest.SendWebRequest();
                if (postrequest.result == UnityWebRequest.Result.Success)
                {
                    string respuestaServidor = postrequest.downloadHandler.text;
                    Debug.Log(respuestaServidor);
                    Navegacion.Instance.ToInitialForm();
                }
                else
                {
                    Debug.Log("Error al registrar jugador");
                }
            }
        }
    }
}
