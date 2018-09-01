using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    
    public float baseSpeed = 1.0f;
    public Vector2 moveDirection = new Vector2();
    public float groundCheckDistance = 0.5f;

    public Transform[] playerSprites = new Transform[2];

    PlayerStates state = PlayerStates.Float;
    float gasCount = 1;
    bool canExpel = true;

    // input variable
    float h_input = 0;// Input.GetAxis("Horizontal");
    bool pressJump = false;// Input.GetButtonDown("Jump");

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    /*void FixedUpdate () {

        if (gasCount > 0)
            canExpel = true;
        else
            canExpel = false;

        if(canExpel)
        {
            playerSprites[0].gameObject.SetActive(true);
            playerSprites[1].gameObject.SetActive(false);
        }

        else
        {
            playerSprites[0].gameObject.SetActive(false);
            playerSprites[1].gameObject.SetActive(true);
        }

        float h_input = Input.GetAxis("Horizontal");
        bool pressJump = Input.GetButtonDown("Jump");

        if(pressJump)
        {
            if (gasCount > 0)
                ExpelGas();
        }

        if(Physics2D.Raycast(transform.position, Vector2.right * h_input, groundCheckDistance))
        {
            h_input = 0;
        }

        float temp_y = moveDirection.y;
        temp_y += GameManager.instance.gravity * Time.deltaTime;*/

    /*if (temp_y >= 20)
        temp_y = 20;*/

    /*if(IsGrounded())
    {
        temp_y = 0;
    }

    moveDirection = new Vector2(h_input, temp_y);
    transform.Translate(moveDirection * baseSpeed * Time.deltaTime);
}*/

    private void FixedUpdate()
    {
        

        if (gasCount > 0)
            canExpel = true;
        else
            canExpel = false;

        if (canExpel)
        {
            playerSprites[0].gameObject.SetActive(true);
            playerSprites[1].gameObject.SetActive(false);
        }

        else
        {
            playerSprites[0].gameObject.SetActive(false);
            playerSprites[1].gameObject.SetActive(true);
        }

        GetInput();

        switch(state)
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

        //moveDirection = new Vector2(h_input, temp_y);
        transform.Translate(moveDirection * baseSpeed * Time.deltaTime);

        if(CheckForSpike())
        {
            Explode();
        }
        
    }

    void GetInput()
    {
        h_input = Input.GetAxis("Horizontal");
        pressJump = Input.GetButtonDown("Jump");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * h_input, groundCheckDistance);
        if (hit.collider != null && hit.collider.tag == "Tile")
        {
            h_input = 0;
        }

        moveDirection.x = h_input;
    }

    void UpdateFloat()
    {
        if (CheckForGoal())
        {
            FoundGoal();
        }

        moveDirection.y += GameManager.instance.gravity * Time.deltaTime;
        if (IsGrounded())
        {
            moveDirection.y = 0;
        }

        if (pressJump)
        {
            ExpelGas();
        }
    }

    void UpdateFall()
    {
        if (CheckForGoal())
        {
            FoundGoal();
        }

        moveDirection.y -= GameManager.instance.gravity * Time.deltaTime;
        if(IsGrounded())
        {
            moveDirection.y = 0;
        }
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
            Debug.Log("HIT A THING");
            if (hit.transform.tag == "Tile")
            {
                Debug.Log("HIT A WALL");
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
            if(hit.collider.name == "Goal")
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
            Debug.Log("Hit a thing");
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

    void ExpelGas()
    {
        gasCount -= 1;
        moveDirection.y = -4;
        GameManager.instance.AddTimer(0.7f, RestoreGas);
        state = PlayerStates.Fall;
    }

    void RestoreGas()
    {
        moveDirection.y = 0;
        state = PlayerStates.Float;
        gasCount += 1;
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
        Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
        //Instantiate(GameManager.instance.popped, transform.position, transform.rotation);
        GameManager.instance.AddTimer(3.5f, GameManager.instance.RestartLevel);
        GameManager.instance.AddTimer(.7f, GameManager.instance.FadeToBlack);
        Destroy(gameObject);
    }

    public enum PlayerStates
    {
        Float,
        Fall,
        Goal
    }
}
