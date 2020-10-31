using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //The renderer used for changing materials;
    private Renderer rend;
    //the material we want to apply
    [SerializeField]
    private Material inMaterial;
    [SerializeField]
    private Material outMaterial;
    public static bool checkpointActif;
    public DeathWall deathwall;

    // Start is called before the first frame update
    void Start()
    {
        //get the renderer we want to change;
        rend = GetComponent<Renderer>();
        checkpointActif = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        rend.material = inMaterial;
        deathwall.teleportTarget.transform.position = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
        rend.material = outMaterial;
        checkpointActif = true;
    }

}
