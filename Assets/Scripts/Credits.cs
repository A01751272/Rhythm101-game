using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This scripts displays the two canvas that compose the credits scene, changing them when a user inputs any key down
 */

public class Credits : MonoBehaviour
{
    public GameObject CongratsPanel;
    public GameObject CreditsPanel;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            CongratsPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }
    }
}
