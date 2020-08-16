using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArea : MonoBehaviour
{
    public Fox Fox;

    private Rigidbody2D FoxRigid;

    // Start is called before the first frame update
    void Start()
    {
        FoxRigid = Fox.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            FoxRigid.velocity = new Vector2(0, FoxRigid.velocity.y);
            if(this.gameObject.tag == "Left")
            {
                Fox.isOnLeftWall = true;
            }
            if (this.gameObject.tag == "Right")
            {
                Fox.isOnRightWall = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            FoxRigid.velocity = new Vector2(0, FoxRigid.velocity.y);
            if (this.gameObject.tag == "Left")
            {
                Fox.isOnLeftWall = false;
            }
            if (this.gameObject.tag == "Right")
            {
                Fox.isOnRightWall = false;
            }
        }
    }
}
