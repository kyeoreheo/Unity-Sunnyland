using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Fox : MonoBehaviour
{
    public Text scoreText;
    public float speed;
    public float jumpSpeed;
    public int cherryPoints;
    public bool isOnWall;
    public bool isMovingRight;

    private Animator myAnimator;
    private Rigidbody2D myBody;
    private bool isOnGround;
    private Vector3 myPosition;
    // Start is called before the first frame update
    void Start()
    {
        isMovingRight = false;
        isOnWall = false;
        isOnGround = false;
        myAnimator = this.gameObject.GetComponent<Animator>();
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
        scoreText.text = cherryPoints.ToString();
        myPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround && myBody.velocity.x == 0.0f)
        {
            myAnimator.Play("Player_idle", 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !(isOnWall && !isMovingRight))
        {
            isMovingRight = false;
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            if (isOnGround)
            {
                myAnimator.Play("Player_Run", 0);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && !(isOnWall && isMovingRight))
        {
            isMovingRight = true;
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            if (isOnGround)
            {
                myAnimator.Play("Player_Run", 0);
            }
        }
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            this.jump(jumpSpeed);
        }
    }
    public void jump(float jumpSpeed)
    {
        myBody.velocity = Vector2.up * jumpSpeed * 2;
        myBody.gravityScale = 3;
        isOnGround = false;
        myAnimator.Play("Player_Jump", 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if(myPosition.x > other.gameObject.transform.position.x)
            myBody.gravityScale = 10;
            isOnGround = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            cherryPoints++;
            scoreText.text = cherryPoints.ToString();
            Destroy(other.gameObject);
        }
    }
}
