using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float doRotate;
    public float playerDistance;

    public GameObject player;

    public enum EnemyState
    {
        Idle,
        BattleIdle,
        Dodgeing,
        Moving,
        Attacking
    }

    [SerializeField]
    EnemyState currentState = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        doRotate = 0;
    }

    // Update is called once per frame
    void Update()
    {

        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance < 10)
        {
            currentState = EnemyState.BattleIdle;
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                idle();
                break;
            case EnemyState.BattleIdle:
                battleIdle();
                break;
            case EnemyState.Dodgeing:
                dodge();
                break;
            case EnemyState.Moving:
                move();
                break;
            case EnemyState.Attacking:
                attack();
                break;
            default:
                break;
        }
    }

    public void idle()
    {
        doRotate = Random.Range(0, 3000);
        if (doRotate == 69)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    public void battleIdle()
    {
        doRotate = Random.Range(0, 3000);
    }

    public void dodge()
    {
        doRotate = 7;
    }

    public void move()
    {
        doRotate = 7;
    }

    public void attack()
    {
        doRotate = 7;
    }
}
