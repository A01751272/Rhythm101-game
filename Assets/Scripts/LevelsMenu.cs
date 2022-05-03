using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Authors:
 * Aleny Sof�a Ar�valo Magdaleno
 * Pablo Gonz�lez de la Parra
 * Luis Humberto Romero P�rez
 * Valeria Mart�nez Silva
 * 
 * Description:
 * This script is responsible for "blocking" levels in the level menu scene if the player hasn's completed a previous required level
 */

public class LevelsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonLevel2;
    [SerializeField]
    private GameObject ButtonLevel3;
    // Start is called before the first frame update
    void Start()
    {
        //Checks whether certain levels showed to be unlocked based on player's attempts
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
