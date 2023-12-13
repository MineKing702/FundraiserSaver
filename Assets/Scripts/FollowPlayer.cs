using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Transform frontTrees;
    public Transform backTrees;
    public Transform bushes;

    public float edgeGuard;
    public float speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        edgeGuard = 2f;
    }
    
    // Update is called once per frame
    void Update()
    {

        //1.29
        Vector3 newPosition = transform.position;
        newPosition.y = player.position.y + 2.282f;
        transform.position = newPosition;
        if (transform.position.z - player.position.z > edgeGuard)
        {
            if (player.position.z < transform.position.z)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                frontTrees.transform.Translate(Vector3.right * 2 * Time.deltaTime);
                bushes.transform.Translate(Vector3.right * 0.1f * Time.deltaTime);
                backTrees.transform.Translate(Vector3.right * 1 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                frontTrees.transform.Translate(Vector3.left * 2 * Time.deltaTime);
                bushes.transform.Translate(Vector3.left * 0.1f * Time.deltaTime);
                backTrees.transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }

        }

        if (transform.position.z - player.position.z < (edgeGuard * -1)) 
        {
            if (player.position.z < transform.position.z)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                frontTrees.transform.Translate(Vector3.right * 2 * Time.deltaTime);
                bushes.transform.Translate(Vector3.right * 0.1f * Time.deltaTime);
                backTrees.transform.Translate(Vector3.right * 1 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                frontTrees.transform.Translate(Vector3.left * 2 * Time.deltaTime);
                bushes.transform.Translate(Vector3.left * 0.1f * Time.deltaTime);
                backTrees.transform.Translate(Vector3.left * 1 * Time.deltaTime);
            }
            
        }
        
        
    }
}
