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
    private string URLId;
    private string playerId;

    private string URLValidarJugador;
    public string idPlayerForm;
    public SignIn Instance;

    public GameObject errorpanel;
    void Awake()
    {
        Instance = this;
    }
    public void Update()
    {
        if (Input.anyKey)
        {
            errorpanel.SetActive(false);
        }
    }
    public void InsertarDatosRegistro()
    {
        StartCoroutine(InsertNewPlayer());
    }

    private IEnumerator InsertNewPlayer()
    {
        if (InputFieldName.text == "" || 
            InputFieldBirthday.text == "" ||
            InputFieldCountry.text == "" ||
            InputFieldCity.text == "" ||
            InputFieldUsername.text == "" ||
            InputFieldPassword.text == "")
        {
            errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Please fill al inputs before signing in";
            errorpanel.SetActive(true);
            Debug.Log("Please fill al inputs before signing in");
        }
        else
        {
            string username = InputFieldUsername.text;

            URLValidarJugador = "https://rhythm101-oxy65.ondigitalocean.app/players/" + username;
            UnityWebRequest request = UnityWebRequest.Get(URLValidarJugador);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "This username already exists. Try another one";
                errorpanel.SetActive(true);
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

                    URLId = "https://rhythm101-oxy65.ondigitalocean.app/players/" + username + "/" + password;
                    UnityWebRequest secondrequest = UnityWebRequest.Get(URLId);
                    yield return secondrequest.SendWebRequest();

                    if (secondrequest.result == UnityWebRequest.Result.Success)
                    {
                        playerId = secondrequest.downloadHandler.text;
                        PlayerPrefs.SetString("idPlayer", playerId);
                        PlayerPrefs.Save();
                        Debug.Log("Se obtiene el id de usuario");
                    }
                    else
                    {
                        errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid input information. Please check and try again";
                        errorpanel.SetActive(true);
                        Debug.Log("Los datos ingresados no son correctos. Revisarlos y volver a intentar");
                    }
                    Navegacion.Instance.ToInitialForm();
                }
                else
                {
                    errorpanel.GetComponentInChildren<TextMeshProUGUI>().text = "Error registering player";
                    errorpanel.SetActive(true);
                    Debug.Log("Error al registrar jugador");
                }
            }
        }
    }

    public void showId()
    {
        StartCoroutine(SearchId());
    }

    public IEnumerator SearchId()
    {
            string username = InputFieldUsername.text;
            string password = InputFieldPassword.text;

            URLId = "https://rhythm101-oxy65.ondigitalocean.app/players/" + username + "/" + password;
            UnityWebRequest request = UnityWebRequest.Get(URLId);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                playerId = request.downloadHandler.text;
                PlayerPrefs.SetString("idPlayer", playerId);
                PlayerPrefs.Save();
                Debug.Log("Se obtiene el id de usuario");
            }
            else
            {
                Debug.Log("Los datos ingresados no son correctos. Revisarlos y volver a intentar");
            }
    }
}
