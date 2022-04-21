using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

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
    private int player;

    public void InsertarDatosFormInicial()
    {
        StartCoroutine(InsertFormInicial());
    }

    public void InsertarDatosFormFinal()
    {
        StartCoroutine(InsertFormFinal());
    }

    private IEnumerator InsertFormInicial()
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

        player = 1; //GetCurrentIdPlayer

        WWWForm Forma = new WWWForm();
        Forma.AddField("number", formnumber1);
        Forma.AddField("ability", ability);
        Forma.AddField("opinion", opinion);
        Forma.AddField("player", player);

        UnityWebRequest postrequest = UnityWebRequest.Post("http://localhost:3000/evaluations/", Forma);
        yield return postrequest.SendWebRequest();
        if (postrequest.result == UnityWebRequest.Result.Success)
        {
            string respuestaServidor = postrequest.downloadHandler.text;
            Debug.Log(respuestaServidor);
            Navegacion.Instance.ToLogin();
        }
        else
        {
            Debug.Log("Error al registrar form");
        }
            
        
    }

    private IEnumerator InsertFormFinal()
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

        player = 1; //CurrentIdPlayer

        WWWForm Forma = new WWWForm();
        Forma.AddField("number", formnumber2);
        Forma.AddField("ability", ability);
        Forma.AddField("opinion", opinion);
        Forma.AddField("player", player);

        UnityWebRequest postrequest = UnityWebRequest.Post("http://localhost:3000/evaluations/", Forma);
        yield return postrequest.SendWebRequest();
        if (postrequest.result == UnityWebRequest.Result.Success)
        {
            string respuestaServidor = postrequest.downloadHandler.text;
            Debug.Log(respuestaServidor);
            Navegacion.Instance.ToCredits();
        }
        else
        {
            Debug.Log("Error al registrar form 1");
        }


    }
}
