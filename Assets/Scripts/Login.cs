using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public TMP_InputField InputFieldUsername;
    public TMP_InputField InputFieldPassword;

    private string URLInicioSesion;
    public string idPlayer;
    public Login Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ValidarDatosInicioSesion()
    {
        StartCoroutine(ValidateExistingPlayerShowId());
    }

    private IEnumerator ValidateExistingPlayerShowId()
    {
        if (InputFieldUsername.text == "" || InputFieldPassword.text == "")
        {
            Debug.Log("Please fill al inputs before signing in");
        }
        else
        {
            string username = InputFieldUsername.text;
            string password = InputFieldPassword.text;

            URLInicioSesion = "http://localhost:3000/players/" + username + "/" + password;
            UnityWebRequest request = UnityWebRequest.Get(URLInicioSesion);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                idPlayer = request.downloadHandler.text;
                Debug.Log("Los datos ingresados son correctos");
                Debug.Log(idPlayer);
                Navegacion.Instance.ToLevelMenu();
            }
            else
            {
                Debug.Log("Los datos ingresados no son correctos. Revisarlos y volver a intentar");
            }
        }
    }
}
