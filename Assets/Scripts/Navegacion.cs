using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Navegacion : MonoBehaviour
{
    // Declaration of panels used to navigate
    [SerializeField]
    private GameObject HUDTutorial;
    [SerializeField]
    private GameObject HUDPause;
    [SerializeField]
    private GameObject TutorialPanel1;
    [SerializeField]
    private GameObject TutorialPanel2;
    private bool TutorialOn = false;
    private bool Panel1On = true;
    private bool Panel2On = false;
    private bool isPaused = false;
    private string score;
    private string level;
    private string BeginTime;
    private string EndTime;
    private string player;

    public static Navegacion Instance;

    void Awake()
    {
        Instance = this;
    }


    // Navigation functions to toggle scenes and/or panels
    // Toggle Scenes
    public void ToLogin()
    {
        SceneManager.LoadScene("Login");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ToLevelMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
    public void ToRankings()
    {
        SceneManager.LoadScene("Ranking");
    }
    public void ToSignIn()
    {
        SceneManager.LoadScene("SignIn");
    }
    public void ToInitialForm()
    {
        SceneManager.LoadScene("InitialForm");
    }
    public void ToFinalForm()
    {
        SceneManager.LoadScene("FinalForm");
    }
    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void ToInterlude1()
    {
        SceneManager.LoadScene("FirstInterlude");
    }
    public void ToInterlude2()
    {
        Debug.Log("Datos recibidos antes de navegar al Interlude 2");
        Debug.Log(AdministradorNivel.instancia.PuntajeActual);
        Debug.Log(AdministradorNivel.instancia.HoraInicio);
        Debug.Log(AdministradorNivel.instancia.HoraFinal);
        InsertarAttemptNivelUno();
        //SceneManager.LoadScene("SecondInterlude");
    }

    public void InsertarAttemptNivelUno()
    {
        level = "1";
        StartCoroutine(InsertAttempt(level));
    }

    private IEnumerator InsertAttempt(string level)
    {
        score = (AdministradorNivel.instancia.PuntajeActual).ToString();
        BeginTime = AdministradorNivel.instancia.HoraInicio;
        EndTime = AdministradorNivel.instancia.HoraFinal;
        player = PlayerPrefs.GetString("idPlayer", "0");

        WWWForm Forma = new WWWForm();
        Forma.AddField("score", score);
        Forma.AddField("level", level);
        Forma.AddField("BeginTime", BeginTime);
        Forma.AddField("EndTime", EndTime);
        Forma.AddField("player", player);

        UnityWebRequest postrequest = UnityWebRequest.Post("http://localhost:3000/attempts/", Forma);
        yield return postrequest.SendWebRequest();
        if (postrequest.result == UnityWebRequest.Result.Success)
        {
            string respuestaServidor = postrequest.downloadHandler.text;
            Debug.Log(respuestaServidor);
        }
        else
        {
            Debug.Log("Error al registrar attempt");
        }
    }

    public void ToInterlude3()
    {
        SceneManager.LoadScene("ThirdInterlude");
    }
    public void ToLevel1()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void ToLevel2()
    {
        SceneManager.LoadScene("LevelTwo");
    }
    public void ToLevel3()
    {
        SceneManager.LoadScene("LevelThree");
    }
    public void RegisterUser()
    {
        // Connects to database
        ToLogin();
    }

    // Toggle panels
    public void DisplayTutorial()
    {
        TutorialOn = !TutorialOn;
        HUDTutorial.SetActive(TutorialOn);
    }
    public void ChangeTutorialPanels()
    {
        Panel1On = !Panel1On;
        Panel2On = !Panel2On;
        TutorialPanel1.SetActive(Panel1On);
        TutorialPanel2.SetActive(Panel2On);
    }
    public void DisplayPause()
    {
        isPaused = !isPaused;
        HUDPause.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    // Exits application
    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        // Displays Pause panel when the escape key is clicked
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisplayPause();
        }
    }
    
}
