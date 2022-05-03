using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/* This script is responsible for the navegation between scenes. Each function is applied to every button, in order to acces the flow of the user
 * throughout the videogame, validating certain requirements such as max level and highest score
 */

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
    public bool isPaused = false;
    private string score;
    private string level;
    private string BeginTime;
    private string EndTime;
    private string player;
    private string MaxLevel;

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

        if (AdministradorNivel.instancia.PuntajeActual <= MovimientoCirculos.Instance.MinimumScore)
        {
            SceneManager.LoadScene("LevelsMenu");
        }
        else
        {
            Debug.Log("Datos recibidos antes de navegar al Final Form");
            Debug.Log(AdministradorNivel.instancia.PuntajeActual);
            Debug.Log(AdministradorNivel.instancia.HoraInicio);
            Debug.Log(AdministradorNivel.instancia.HoraFinal);
            InsertarAttemptNivelThree();

            MaxLevel = PlayerPrefs.GetString("idLevel");
            if (MaxLevel == "3")
            {
                SceneManager.LoadScene("Credits");
            }
            else
            {
                PlayerPrefs.SetString("idLevel", "3");
                SceneManager.LoadScene("FinalForm");
            }
        }

        
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
        if (AdministradorNivel.instancia.PuntajeActual <= MovimientoCirculos.Instance.MinimumScore)
        {
            SceneManager.LoadScene("LevelsMenu");
        }
        else
        {
            Debug.Log("Datos recibidos antes de navegar al Interlude 2");
            Debug.Log(AdministradorNivel.instancia.PuntajeActual);
            Debug.Log(AdministradorNivel.instancia.HoraInicio);
            Debug.Log(AdministradorNivel.instancia.HoraFinal);
            InsertarAttemptNivelUno();
            PlayerPrefs.SetString("idLevel", "1");
            SceneManager.LoadScene("SecondInterlude");
        }
    }

    public void ToInterlude2LevelMenu()
    {
        if(Int32.Parse(PlayerPrefs.GetString("idLevel")) >= 1)
        {
            SceneManager.LoadScene("SecondInterlude");
        }
    }

    public void InsertarAttemptNivelUno()
    {
        level = "1";
        StartCoroutine(InsertAttempt(level));
    }

    public void InsertarAttemptNivelDos()
    {
        level = "2";
        StartCoroutine(InsertAttempt(level));
    }

    public void InsertarAttemptNivelThree()
    {
        level = "3";
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

        UnityWebRequest postrequest = UnityWebRequest.Post("https://rhythm101-oxy65.ondigitalocean.app/attempts/", Forma);
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
        if (AdministradorNivel.instancia.PuntajeActual <= MovimientoCirculos.Instance.MinimumScore)
        {
            SceneManager.LoadScene("LevelsMenu");
        }
        else
        {
            Debug.Log("Datos recibidos antes de navegar al Interlude 3");
            Debug.Log(AdministradorNivel.instancia.PuntajeActual);
            Debug.Log(AdministradorNivel.instancia.HoraInicio);
            Debug.Log(AdministradorNivel.instancia.HoraFinal);
            InsertarAttemptNivelDos();
            PlayerPrefs.SetString("idLevel", "2");
            SceneManager.LoadScene("ThirdInterlude");
        }
    }

    public void ToInterlude3LevelMenu()
    {
        if (Int32.Parse(PlayerPrefs.GetString("idLevel")) >= 2)
        {
            SceneManager.LoadScene("ThirdInterlude");
        }
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
        //Pause music of the level
        if (isPaused)
        {
            AdministradorNivel.instancia.CancionNivel.Pause();
        } else
        {
            AdministradorNivel.instancia.CancionNivel.Play();
        }
    }

    //Restart level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
