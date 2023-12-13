using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController1 : MonoBehaviour
{
    public float enemyHP;
    public float timesWalked;
    public float speed = 1.5f;

    public enum EnemyState
    {
        idle,
        attacking, 
        dead,
    }

    [SerializeField]
    EnemyState currentState = EnemyState.idle;

    // Start is called before the first frame update
    void Start()
    {
        timesWalked = 0f;
        enemyHP = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timesWalked > 500f)
        {
            // code to rotate
        }
        else
        {
            timesWalked++;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
