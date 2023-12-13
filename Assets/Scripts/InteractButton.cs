using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{

    public GameObject button;
    public Transform player;
    public Transform testChild;

    public float interactDistance;

    // Start is called before the first frame update
    void Start()
    {
        interactDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(player.transform.position, testChild.transform.position);
        if (distance < interactDistance)
        { 
            button.SetActive(true);
           
        }
        else
        {
            button.SetActive(false);    
        }
           
    }
}
