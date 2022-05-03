using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Authors:
 * Aleny Sof�a Ar�valo Magdaleno
 * Pablo Gonz�lez de la Parra
 * Luis Humberto Romero P�rez
 * Valeria Mart�nez Silva
 * 
 * Description: 
 * This scripts displays the two canvas that compose the credits scene, changing them when a user inputs any key down
 */

public class Credits : MonoBehaviour
{
    public GameObject CongratsPanel;
    public GameObject CreditsPanel;
    // Update is called once per frame
    void Update()
    {
        //If any key is pressed, jump to the second Panel from Credits main canvas
        if (Input.anyKey)
        {
            CongratsPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }
    }
}
