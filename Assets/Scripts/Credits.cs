using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
