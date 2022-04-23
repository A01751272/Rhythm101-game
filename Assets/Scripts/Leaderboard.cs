using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    private string LeaderboardURL = "http://localhost:3000/attempts/leaderboard";
    private string respuestaServidor;
    void Start()
    {
        searchLeaderboard();
    }

    public void searchLeaderboard()
    {
        StartCoroutine(showLeaderboard());
    }

    public LeaderboardData leadData;

    public struct LeaderboardData
    {
        public string id;
    }

    private IEnumerator showLeaderboard()
    {
        UnityWebRequest request = UnityWebRequest.Get(LeaderboardURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            respuestaServidor = request.downloadHandler.text;
            Debug.Log(respuestaServidor);
            leadData = JsonUtility.FromJson<LeaderboardData>("{\"id\":" + respuestaServidor + "}");
            Debug.Log(leadData);
        }
        else
        {
            Debug.Log("Error al enviar los datos");
        }
    }
            
}
