using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Authors:
 * Aleny Sofía Arévalo Magdaleno
 * Pablo González de la Parra
 * Luis Humberto Romero Pérez
 * Valeria Martínez Silva
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
