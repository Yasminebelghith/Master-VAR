using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndZone : MonoBehaviour
{
    public Text victoryText;

    private void Start()
    {
        // to access the value of count 
        GetComponent<Unity101_CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && Unity101_CharacterController.count == 5)
        {
            victoryText.text = "You won !";
            other.SendMessage("stopTimer");
        }
    }
}
