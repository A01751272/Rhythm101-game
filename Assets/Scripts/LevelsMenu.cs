using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonLevel2;
    [SerializeField]
    private GameObject ButtonLevel3;
    // Start is called before the first frame update
    void Start()
    {
        switch (Int32.Parse(PlayerPrefs.GetString("idLevel")))
        {
            case 0:
                ButtonLevel2.GetComponent<Image>().color = Color.gray;
                ButtonLevel3.GetComponent<Image>().color = Color.gray;
                break;
            case 1:
                ButtonLevel3.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }
    }

}
