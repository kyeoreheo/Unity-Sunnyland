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
    public int score;
    public bool isOnRightWall;
    public bool isOnLeftWall;

    private Animator myAnimator;
    private Rigidbody2D myBody;
    private bool isOnGround;
    private Vector3 myPosition;
    // Start is called before the first frame update
    void Start()
    {
        isOnRightWall = false;
        isOnLeftWall = false;
        isOnGround = false;
        myAnimator = this.gameObject.GetComponent<Animator>();
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
        scoreText.text = score.ToString();
        myPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround && myBody.velocity.x == 0.0f)
        {
            myAnimator.Play("Player_idle", 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !isOnRightWall)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            if (isOnGround)
            {
                myAnimator.Play("Player_Run", 0);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isOnLeftWall)
        {
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
            score++;
            scoreText.text = score.ToString();
            Destroy(other.gameObject);
        }
    }
}
