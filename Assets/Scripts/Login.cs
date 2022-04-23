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
    private string URLNivel;
    public string idPlayer;
    public Login Instance;
    private string playerId;
    private string levelMax;
    public string playerprefId;


    public RespuestaNivel resNivel;


    void Awake()
    {
        Instance = this;
    }

    public struct RespuestaNivel
    {
        public string MaxLevel;
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
                playerId = request.downloadHandler.text;
                PlayerPrefs.SetString("idPlayer", playerId);
                PlayerPrefs.Save();
                Debug.Log("Los datos ingresados son correctos");

                idPlayer = PlayerPrefs.GetString("idPlayer", "0");
                Debug.Log("IdPlayer: " + idPlayer);
                URLNivel = "http://localhost:3000/attempts/" + idPlayer;
                UnityWebRequest nivelrequest = UnityWebRequest.Get(URLNivel);
                yield return nivelrequest.SendWebRequest();
                if (nivelrequest.result == UnityWebRequest.Result.Success)
                {
                    levelMax = nivelrequest.downloadHandler.text;
                    resNivel = JsonUtility.FromJson<RespuestaNivel>(levelMax);

                    if (resNivel.MaxLevel == "")
                    {
                        PlayerPrefs.SetString("idLevel", "1");
                        PlayerPrefs.Save();
                        playerprefId = PlayerPrefs.GetString("idLevel");
                        Debug.Log("El nivel máximo es: " + playerprefId);

                    }
                    else
                    {
                        PlayerPrefs.SetString("idLevel", resNivel.MaxLevel.ToString());
                        PlayerPrefs.Save();
                        playerprefId = PlayerPrefs.GetString("idLevel");
                        Debug.Log("El nivel máximo es: " + playerprefId);
                    }
                    
                }
                else
                {
                    Debug.Log("Fallo en obtener el nivel máximo");
                }

                Navegacion.Instance.ToLevelMenu();
            }
            else
            {
                Debug.Log("Los datos ingresados no son correctos. Revisarlos y volver a intentar");
            }
        }
    }
}
