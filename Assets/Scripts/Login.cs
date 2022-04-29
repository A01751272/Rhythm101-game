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

    public GameObject panelerror;


    public RespuestaNivel resNivel;


    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            panelerror.SetActive(false);
        }
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
            panelerror.GetComponentInChildren<TextMeshProUGUI>().text = "Please fill al inputs before sending the form.";
            panelerror.SetActive(true);
            Debug.Log("Please fill all inputs before signing in");
        }
        else
        {
            string username = InputFieldUsername.text;
            string password = InputFieldPassword.text;

            URLInicioSesion = "https://rhythm101-oxy65.ondigitalocean.app/players/" + username + "/" + password;
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
                URLNivel = "https://rhythm101-oxy65.ondigitalocean.app/attempts/" + idPlayer;
                UnityWebRequest nivelrequest = UnityWebRequest.Get(URLNivel);
                yield return nivelrequest.SendWebRequest();
                if (nivelrequest.result == UnityWebRequest.Result.Success)
                {
                    levelMax = nivelrequest.downloadHandler.text;
                    resNivel = JsonUtility.FromJson<RespuestaNivel>(levelMax);

                    if (resNivel.MaxLevel == "")
                    {
                        PlayerPrefs.SetString("idLevel", "0");
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

                Navegacion.Instance.ToMainMenu();
            }
            else
            {
                panelerror.GetComponentInChildren<TextMeshProUGUI>().text = "Incorrect username or password, please check and try again. If you don't have an account try the sign in option.";
                panelerror.SetActive(true);
                Debug.Log("Los datos ingresados no son correctos. Revisarlos y volver a intentar");
            }
        }
    }
}
