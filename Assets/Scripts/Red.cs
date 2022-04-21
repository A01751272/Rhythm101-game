using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;

public class Red : MonoBehaviour
{
    public TextMeshProUGUI TextoResultado;

    public TMP_InputField InputNombreRegistro;
    public TMP_InputField InputPuntosRegistro;

    public DatosUsuario datos;

    public string URL;

    public struct DatosUsuario
    {
        public string nombre;
        public int puntos;
    }

    public void EnviarJSON()
    {
        StartCoroutine(SubirJSON());
    }

    public void LeerJSON()
    {
        StartCoroutine(DescargarJSON());
    }

    private IEnumerator DescargarJSON()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/pruebaUnity/enviaJson.php");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string respuestaServidor = request.downloadHandler.text;
            TextoResultado.text = respuestaServidor;
            datos = JsonUtility.FromJson<DatosUsuario>(respuestaServidor);
            TextoResultado.text = TextoResultado.text + "\nNombre:" + datos.nombre;
        }
        else
        {
            TextoResultado.text = "Error: " + request.ToString();
        }
    }

    private IEnumerator SubirJSON()
    {
        datos.nombre = InputNombreRegistro.text;
        datos.puntos = int.Parse(InputPuntosRegistro.text);
        print(JsonUtility.ToJson(datos));

        WWWForm Forma = new WWWForm();
        Forma.AddField("datosJSON", JsonUtility.ToJson(datos));
        UnityWebRequest request = UnityWebRequest.Post("http://localhost/pruebaUnity/recibeJSON.php", Forma);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string respuestaServidor = request.downloadHandler.text;
            TextoResultado.text = respuestaServidor;
        }
        else
        {
            TextoResultado.text = "Error: " + request.ToString();
        }
    }


    public void EnviarTextoPlano()
        {
            StartCoroutine(SubirTextoPlano());
        }

    private IEnumerator SubirTextoPlano()
    {
        string nombre = InputNombreRegistro.text;
        string puntos = InputPuntosRegistro.text;

        WWWForm Forma = new WWWForm();

        Forma.AddField("nombre", nombre);
        Forma.AddField("puntos", puntos);

        UnityWebRequest request = UnityWebRequest.Post("http://localhost/pruebaUnity/respuestaServidor.php", Forma);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            string respuestaServidor = request.downloadHandler.text;
            TextoResultado.text = respuestaServidor;
        }
        else
        {
            TextoResultado.text = "Error: " + request.ToString();
        }
    }

    public void LeerTextoPlano()
    {
        StartCoroutine(DescargarDatosRed());
    }
    private IEnumerator DescargarDatosRed()
    {
        string nombre = InputNombreRegistro.text;
        string puntos = InputPuntosRegistro.text;

        URL = "http://localhost:3000/accounts/" + nombre + "/" + puntos;
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success)
        {
            string textoResultado = request.downloadHandler.text;
            TextoResultado.text = textoResultado;
            if(textoResultado == puntos)
            {
                TextoResultado.text = "Contraseña válida";
            } else
            {
                TextoResultado.text = "Contraseña no válida";
            }

        } else
        {
            TextoResultado.text = "Los datos ingresados no son correctos";
        }
    }
}
