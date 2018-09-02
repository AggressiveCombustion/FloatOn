using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMenu : MonoBehaviour
{

    public float baseSpeed = 1.0f;
    public Vector2 moveDirection = new Vector2();
    public float groundCheckDistance = 0.5f;

    public Transform[] playerSprites = new Transform[3];

    public GameObject playerInMenu;

    public PlayerStates state = PlayerStates.Float;
    float gasCount = 1;

    public float gasAmount = 0.0f;
    public float gasRate = 0.1f;
    bool canExpel = true;

    // input variable
    float h_input = 0;// Input.GetAxis("Horizontal");
    bool pressJump = false;// Input.GetButtonDown("Jump");
    public  GameObject belch;

    // Use this for initialization
    void Start()
    {
        name = "Player In Menu";
        DontDestroyOnLoad(gameObject);
    }

    void HandleGas()
    {
        gasAmount += gasRate * Time.deltaTime;
        if(gasAmount > 2.5f)
            //Explode();
            gasAmount = 2.5f;

        playerSprites[0].gameObject.SetActive(gasAmount >= 1);
        if (gasAmount >= .4f)
            state = PlayerStates.Float;
        playerSprites[1].gameObject.SetActive(gasAmount <= 0.4f);
        playerSprites[2].gameObject.SetActive(gasAmount < 1 && gasAmount > 0.4f);

        //GameObject.Find("arrow").GetComponent<SpriteRenderer>().enabled = gasAmount >= 1.0f;
        GameObject.Find("target").GetComponentInChildren<SpriteRenderer>().enabled = gasAmount >= 1.0f;
        GameObject.Find("target").GetComponent<SpriteRenderer>().enabled = gasAmount >= 1.0f;
    }

    private void FixedUpdate()
    {

        GetInput();

        switch (state)
        {
            case PlayerStates.Float:
                UpdateFloat();
                break;
            case PlayerStates.Fall:
                UpdateFall();
                break;
            case PlayerStates.Goal:
                UpdateGoal();
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                           moveDirection,
                                           groundCheckDistance);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Tile")
            {
                moveDirection = Vector2.zero;
            }
        }

        //moveDirection = new Vector2(h_input, temp_y);
        transform.Translate(moveDirection * baseSpeed * Time.deltaTime, Space.World);
        //transform.Translate(moveDirection * Vector2.up);

        if (CheckForSpike())
        {
            Explode();
        }
        
        // die if we're too far up
        if(transform.position.y > 7)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.y = -6.7f;
            spawnPos.x = Random.Range(-7, 7);
            spawnPos.z = -9.5f;
            GameObject g = Instantiate(playerInMenu, spawnPos, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }

    }

    void GetInput()
    {
        pressJump = Input.GetButtonDown("Fire1");

    }

    void UpdateFloat()
    {
        HandleGas();

        // rotate against mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.Rotate(Vector3.forward, Vector3.Angle(mousePos, transform.position));
        float distance = Vector2.Distance(mousePos, transform.position);

        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y,
                                     mousePos.x - transform.position.x);
        float angle = (180 / Mathf.PI) * angleRad;

        transform.rotation = Quaternion.Euler(0, 0, angle + 270);

        //Debug.Log(mousePos.x);

        if (CheckForGoal())
        {
            FoundGoal();
        }

        /*
        moveDirection.y += GameManager.instance.gravity * Time.deltaTime;
        if (IsGrounded())
        {
            moveDirection.y = 0;
        }*/

        moveDirection = Vector2.up * GameManager.instance.gravity;

        if (pressJump && gasAmount >= 1)
        {
            ExpelGas();
        }


    }

    void UpdateFall()
    {
        HandleGas();

        if (CheckForGoal())
        {
            FoundGoal();
        }

        /*moveDirection.y -= GameManager.instance.gravity * Time.deltaTime;
        if (IsGrounded())
        {
            moveDirection.y = 0;
        }*/
    }

    void ExpelGas()
    {
        gasCount -= 1;
        //moveDirection.y = -4;
        moveDirection = -transform.up * 4;
        GameManager.instance.AddTimer(0.7f, RestoreGas);
        state = PlayerStates.Fall;
        gasAmount = 0.0f;

        Vector2 pos = /*GameObject.Find("target").*/transform.position;
        //GameObject b = Instantiate(belch, pos, Quaternion.Euler(0, 0, 0));
    }

    void RestoreGas()
    {
        moveDirection.y = 0;
        state = PlayerStates.Float;
        gasCount += 1;
    }

    void UpdateGoal()
    {
        moveDirection = Vector2.zero;
        transform.position = Vector2.Lerp(transform.position,
                                          GameObject.Find("Goal").transform.position,
                                          baseSpeed * Time.deltaTime * GameManager.instance.speed);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up * Mathf.Sign(moveDirection.y), groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            //Debug.Log("HIT A THING");
            if (hit.transform.tag == "Tile")
            {
                //Debug.Log("HIT A WALL");
                return true;
            }
        }

        return false;
    }

    bool CheckForGoal()
    {
        // above
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up * Mathf.Sign(moveDirection.y), groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.name == "Goal")
            {
                return true;
            }
        }

        // below
        hit = Physics2D.Raycast(transform.position, -Vector3.up * Mathf.Sign(moveDirection.y), groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.name == "Goal")
            {
                return true;
            }
        }

        // right
        hit = Physics2D.Raycast(transform.position, Vector3.right * Mathf.Sign(moveDirection.y), groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.name == "Goal")
            {
                return true;
            }
        }

        // left
        hit = Physics2D.Raycast(transform.position, -Vector3.right * Mathf.Sign(moveDirection.y), groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.name == "Goal")
            {
                return true;
            }
        }

        return false;
    }

    bool CheckForSpike()
    {
        // above
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            //Debug.Log("Hit a thing");
            if (hit.collider.tag == "Spike")
            {
                Debug.Log("POP POP");
                return true;
            }
        }

        // below
        hit = Physics2D.Raycast(transform.position, -Vector3.up, groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Spike")
            {
                return true;
            }
        }

        // right
        hit = Physics2D.Raycast(transform.position, Vector3.right, groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Spike")
            {
                return true;
            }
        }

        // left
        hit = Physics2D.Raycast(transform.position, -Vector3.right, groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Spike")
            {
                return true;
            }
        }

        return false;
    }

    bool CheckIfCrushed()
    {
        // above
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Hit a thing");
            if (hit.collider.tag == "Tile")
            {
                // check below
                hit = Physics2D.Raycast(transform.position, -Vector3.up, groundCheckDistance);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Tile")
                    {
                        return true;
                    }
                }
            }
        }

        // right
        hit = Physics2D.Raycast(transform.position, Vector3.right, groundCheckDistance);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.up * groundCheckDistance), Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Hit a thing");
            if (hit.collider.tag == "Tile")
            {
                // check left
                hit = Physics2D.Raycast(transform.position, -Vector3.right, groundCheckDistance);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Tile")
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void FoundGoal()
    {
        GameManager.instance.FinishLevel();
        state = PlayerStates.Goal;
        gameObject.AddComponent<RotateThis>();
        RotateThis r = GetComponent<RotateThis>();
        r.speed = 20;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("HitGoal");
        //GameManager.instance.AddTimer(1.5f, GameManager.instance.FadeToBlack);
    }

    public void Explode()
    {
        Instantiate(GameManager.instance.explosion, transform.position, Quaternion.Euler(0, 0, 0));
        //Instantiate(GameManager.instance.popped, transform.position, transform.rotation);
        //GameManager.instance.AddTimer(3.5f, GameManager.instance.RestartLevel);
        //GameManager.instance.AddTimer(.7f, GameManager.instance.FadeToBlack);
        Destroy(gameObject);
    }

    public enum PlayerStates
    {
        Float,
        Fall,
        Goal
    }
}
