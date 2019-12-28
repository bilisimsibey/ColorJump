using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb; //fizik

    float jumpVelocity; //zıplama hizi
    public float gravity = 1; //player yerçekimi
    public float jumpHeight; //ziplama yuksekligi

    bool isDragging = false;
    Vector2 touchPosition; //dokunma pozisyonu
    Vector2 playerPosition; 
    Vector2 dragPosition;

    stairsManager stairsManager;

    public GameObject jumpEffectPrefab;
    public GameObject deadEffectPrefab;
   

    bool isDead = false;
    bool isStart = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stairsManager = GameObject.Find("Stairs").GetComponent<stairsManager>();
    }

    private void Update()
    {
        WaitToTouch();
        if (isDead) return;
        if (!isStart) return;
        

        
        GetInput();
        MovePlayer();
        AddGravityToPlayer();
        DeadCheck();
    }
    void WaitToTouch()
    {
        if (!isStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isStart = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().GameStart();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Stair")
        {
            if (rb.velocity.y<=0)
            {
                Jump();
                AddScore();
                Effect();
                ChangeBackgroundColor(collision);
                DestroyStairAndMakeStair(collision);
                
            }
            
        }
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            isDragging = true;
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
            playerPosition = transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            
            isDragging = false;
        }
    }

    void MovePlayer()
    {
        if (isDragging)
        {
            dragPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
            transform.position = new Vector2(playerPosition.x+(dragPosition.x-touchPosition.x),transform.position.y);

            if (transform.position.x<-6)
            {
                transform.position = new Vector2(-6,transform.position.y);
            }
            if (transform.position.x>6)
            {
                transform.position = new Vector2(6,transform.position.y);
            }
        }
    }
    void Jump()
    {
        jumpVelocity = gravity * jumpHeight;
        rb.velocity = new Vector2(0, jumpVelocity);
        gravity += 0.01f;
    }

    void AddScore()
    {
        GameObject.Find("GameManager").GetComponent<scoreManager>().addScore(1);
    }
    void DestroyStairAndMakeStair(Collider2D stair)
    {
        Destroy(stair.gameObject);
        stairsManager.MakeNewStair();
    }

    void Effect()
    {

        

        Destroy(Instantiate(jumpEffectPrefab,transform.position,Quaternion.identity),0.5f);

    }
    void AddGravityToPlayer()
    {
        rb.velocity = new Vector2(0,rb.velocity.y-(gravity*gravity));
    }

    void DeadCheck()
    {
        if (isDead==false && transform.position.y<Camera.main.transform.position.y-10)
        {
            isDead = true;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            Destroy(Instantiate(deadEffectPrefab, transform.position, Quaternion.identity), 0.5f);
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

    void ChangeBackgroundColor(Collider2D stair)
    {
        Camera.main.backgroundColor = stair.gameObject.GetComponent<SpriteRenderer>().color;
    }
    
}
