using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPlayer : MonoBehaviour
{
    public GameObject cylinderHelp;

    private void Start()
    {
        cylinderHelp.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cylinderHelp.SetActive(true);
        }
    }
}
