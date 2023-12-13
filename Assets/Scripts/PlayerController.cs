using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float wallForceCountDown;
    public float wallDistance;
    public float speed;
    public float jumpForce;
    public float downForceAD;
    public float dashSpeed;
    public float interactDistance;
    public float dollars;

    public bool interacting;
    public bool onGround;
    public bool canInteract;
    public bool canMove;
    public bool isDashing;
    public bool canDash;
    public bool normalJump;
    public bool running;
    public bool menuScreen;
    public bool onWall;

    public Transform camera;
    public Transform player;
    public Transform spawnPoint;

    public GameObject dollar;
    public GameObject controlSign;
    public GameObject resumeSign;
    public GameObject exitSign;

    public Rigidbody rb;

    public Animator playerAnim;

    public TMP_Text dollarText; // The TextMeshPro object to display


    public Vector3 jump;
    public Vector3 downAfterDollar;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = true;

        controlSign.SetActive(false);
        resumeSign.SetActive(false);
        exitSign.SetActive(false);

        menuScreen = false;
        canInteract = false;
        interacting = false;
        normalJump = true;
        onGround = false;
        onWall = false;
        running = true;
        canDash = true;
        canMove = true;
        running = true;

        wallForceCountDown = -1;
        interactDistance = 2;
        wallDistance = 0.4f;
        downForceAD = -0.5f;
        jumpForce = 2.7f;
        dashSpeed = 30f;
        dollars = 0f;

        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        downAfterDollar = new Vector3(0.0f, -1.0f, 0.0f);
        jump = new Vector3(0.0f, 2.2f, 0.0f);

        playerAnim = gameObject.GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            dollarText.SetText("" + dollars);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, wallDistance))
            {
                if (hit.transform.tag == "wall" && !onWall)
                {
                    Debug.Log("HitWall");
                    rb.useGravity = false;
                    rb.velocity = new Vector3(0, 0, 0);
                    playerAnim.SetBool("onWall", true);
                    onWall = true;
                }
            }


            if (!onWall)
            {
                //this rotates the player left on the left arrow key
                if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }

                //this rotates the plyer righ on the right arrow key
                if (Input.GetKeyDown(KeyCode.RightArrow) && canMove)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                //this bit moves the player left and right with the arrow keys
                if (Input.GetKey(KeyCode.RightArrow) && canMove || Input.GetKey(KeyCode.LeftArrow) && canMove)
                {
                    speed = 3f;

                    playerAnim.SetBool("running", true);

                    transform.Translate(Vector3.forward * speed * Time.deltaTime);

                    if (!onGround)
                    {
                        speed = 3f;
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    }
                }
                else
                {
                    playerAnim.SetBool("running", false);
                }
            }

            //this makes the player jump on the space key
            if (Input.GetKeyDown(KeyCode.Space) && canMove)
            {
                //this gets rid of double jumping
                if (onGround)
                {
                    playerAnim.SetTrigger("Jump");
                    //this moves the player up on jump
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                    onGround = false;
                    playerAnim.SetBool("onGround", false);
                }
                else if (onWall)
                {
                    rb.useGravity = true;
                    transform.RotateAround(transform.position, transform.up, 180f);
                    rb.AddForce((transform.up * 7) + (transform.forward * 6), ForceMode.Impulse);
                    onWall = false;
                    playerAnim.SetBool("onWall", false);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && canDash)
            {
                StartCoroutine(dash());
            }

            if (isDashing)
            {
                transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuScreen)
            {
                menuScreen = true;
                controlSign.SetActive(true);
                resumeSign.SetActive(true);
                exitSign.SetActive(true);
                running = false;
            }
            else
            {
                menuScreen = false;
                controlSign.SetActive(false);
                resumeSign.SetActive(false);
                exitSign.SetActive(false);
                running = true;
            }
        }
    }

    IEnumerator dash()
    {
        canDash = false;
        isDashing = true;
        rb.useGravity = false;
        yield return new WaitForSecondsRealtime(0.15f);
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSecondsRealtime(5);
        canDash = true;
        yield return new WaitForSecondsRealtime(4.5f);
    }



    //this happens when the players box collider hit another box collider
    void OnCollisionEnter(Collision collision)
    {
        //this sees if the thing the player hit wa the ground
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            onWall = false;
            playerAnim.SetBool("onGround", true);
        }
        else if (collision.gameObject.tag == "danger")
        {
            //transform.position = new Vector3(0.845f, 1.003f, 0f);
            transform.position = spawnPoint.position;
            camera.position = new Vector3(28.53f, 3.86f, 8.84f);
            isDashing = false;
            canDash = true;
        }
        else if (collision.gameObject.tag == "wall")
        {
            isDashing = false;
            playerAnim.SetBool("onWall", true);
        }
        else if (collision.gameObject.name == "dollar (1)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (1)");
            dollar.SetActive(false);
            rb.AddForce(downAfterDollar * downForceAD, ForceMode.Impulse);
        }
        else if (collision.gameObject.name == "dollar")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (2)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (2)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (3)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (3)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (4)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (4)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (5)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (5)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (6)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (6)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (7)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (7)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (8)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (8)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (9)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (9)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (10)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (10)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (11)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (11)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (12)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (12)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (13)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (13)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (14)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (14)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (15)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (15)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (16)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (16)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (17)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (17)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (18)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (18)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (19)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (19)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (20)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (20)");
            dollar.SetActive(false);
        }
        else if (collision.gameObject.name == "dollar (21)")
        {
            dollars++;
            Debug.Log(dollars);
            dollar = GameObject.Find("dollar (21)");
            dollar.SetActive(false);
        }
    }
}