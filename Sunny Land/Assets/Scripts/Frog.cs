using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float frogOffset;
    public float moveSpeed;
    public float stopTime;
    public float jumpHeight;
    public bool jump;

    private Rigidbody2D myBody;
    private Animator myAnimator;
    private Vector2 leftMovePosition;
    private Vector2 rightMovePosition;
    private Transform myTransform;
    private int direction;
    private float timer;
    private bool isAtEnd;
    private bool isOnGround;
    // Start is called before the first frame update
    void Start()
    {
        isAtEnd = false;
        direction = -1;
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
        myAnimator = this.gameObject.GetComponent<Animator>();
        myTransform = this.gameObject.transform;
        leftMovePosition = new Vector2(myTransform.position.x - frogOffset, transform.position.y);
        rightMovePosition = new Vector2(myTransform.position.x + frogOffset, transform.position.y);
        myBody.velocity = new Vector2(direction * moveSpeed, myBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (myBody.velocity.x != 0)
        {
            timer = 0.0f;
            if(myTransform.position.x >= rightMovePosition.x )
            {
                direction = -1;
                isAtEnd = true;
            }
            if(myTransform.position.x <= leftMovePosition.x)
            {
                direction = 1;
                isAtEnd = true;
            }
        }

        if(timer < stopTime && isAtEnd)
        {
            myAnimator.Play("Frog_idle", 0);
            myTransform.eulerAngles = new Vector3(0, 90 + 90 * direction, 0);
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }
        else
        {
            isAtEnd = false;
            timer = 0.0f;
            myBody.velocity = new Vector2(direction * moveSpeed, myBody.velocity.y);
            if (jump)
            {
                if (isOnGround)
                {
                    myBody.gravityScale = 3;
                    myAnimator.Rebind();
                    myAnimator.Play("Frog_Jump", 0);
                    isOnGround = false;
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
                }
            }
            else
            {
                myAnimator.Rebind();
                myAnimator.Play("Frog_Jump", 0);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            myBody.gravityScale = 100;
            isOnGround = true;
        }
    }
}
