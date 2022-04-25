using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    private string respuestaServidor;
    private string idPlayer;

    public TextMeshProUGUI scoreFirstLevel;
    public TextMeshProUGUI scoreSecondLevel;
    public TextMeshProUGUI scoreThirdLevel;
    void Start()
    {
        idPlayer = PlayerPrefs.GetString("idPlayer", "0");
        searchLeaderboard();
    }

    public void searchLeaderboard()
    {
        StartCoroutine(showLeaderboard1());
        StartCoroutine(showLeaderboard2());
        StartCoroutine(showLeaderboard3());
    }

    public LeaderboardData leadData;

    public struct LeaderboardData
    {
        public string score;
    }

    private IEnumerator showLeaderboard1()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://rhythm101-oxy65.ondigitalocean.app/attempts/" + idPlayer + "/1");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            respuestaServidor = request.downloadHandler.text;
            Debug.Log(respuestaServidor);
            leadData = JsonUtility.FromJson<LeaderboardData>(respuestaServidor);
            Debug.Log(leadData.score);
            scoreFirstLevel.text = leadData.score;
        }
        else
        {
            Debug.Log("Error al recibir los datos");
            scoreFirstLevel.text = "Not completed yet...";
        }
    }

    private IEnumerator showLeaderboard2()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://rhythm101-oxy65.ondigitalocean.app/attempts/" + idPlayer + "/2");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            respuestaServidor = request.downloadHandler.text;
            Debug.Log(respuestaServidor);
            leadData = JsonUtility.FromJson<LeaderboardData>(respuestaServidor);
            Debug.Log(leadData.score);
            scoreSecondLevel.text = leadData.score;
        }
        else
        {
            Debug.Log("Error al recibir los datos");
            scoreSecondLevel.text = "Not completed yet...";
        }
    }

    private IEnumerator showLeaderboard3()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://rhythm101-oxy65.ondigitalocean.app/attempts/" + idPlayer + "/3");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            respuestaServidor = request.downloadHandler.text;
            Debug.Log(respuestaServidor);
            leadData = JsonUtility.FromJson<LeaderboardData>(respuestaServidor);
            Debug.Log(leadData.score);
            scoreThirdLevel.text = leadData.score;
        }
        else
        {
            Debug.Log("Error al recibir los datos");
            scoreThirdLevel.text = "Not completed yet...";
        }
    }

}
