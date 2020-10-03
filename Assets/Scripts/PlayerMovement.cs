using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D pc;    //player collider
    public BoxCollider2D gc;    //ground collider
    public BoxCollider2D cc;    //crouch collider

    public float walkSpeed = 5f;
    public float jumpPower = 10f;

    public bool canDoubleJump = false;
    public bool canClimb = false;

    public bool facingRight = false;
    public bool grounded = false;
    public bool sprinting = false;
    public bool crouching = false;
    public bool climbing = false;
    public bool doubleJumped = false;

    Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);
    float moveX, moveY, speed;

    void FixedUpdate()
    {
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!sprinting && !crouching && grounded) sprinting = true;
        }
        else
        {
            sprinting = false;
        }

        speed = walkSpeed * (sprinting ? 2f : 1f);

        //Check Grounded
        List <Collider2D> gr = new List<Collider2D>();
        gc.OverlapCollider(new ContactFilter2D(), gr);
        grounded = (gr.Count > 0);

        //Get Axis Input
        moveX = Input.GetAxis("Horizontal"); 
        moveY = Input.GetAxis("Vertical");

        //Direction
        if ((facingRight && moveX < 0) || (!facingRight && moveX > 0))
        {
            sprinting = false;
            facingRight = !facingRight;
        }

        //Climbing
        if (canClimb)
        {
            RaycastHit2D hit = Physics2D.Raycast(getLadderRaycastOrigin(), (facingRight ? Vector2.right : Vector2.left), 0.05f);
            if (!climbing && !crouching && hit.collider != null && hit.collider.gameObject.tag == "Climbable" && Mathf.Abs(moveX) > 0.01f) startClimbing();
            if (climbing && (hit.collider == null || hit.collider.gameObject.tag != "Climbable")) 
            {
                RaycastHit2D backhit = Physics2D.Raycast(getLadderRaycastOrigin(true), (facingRight ? Vector2.left : Vector2.right), 0.1f);
                if (backhit.collider == null || backhit.collider.gameObject.tag != "Climbable") stopClimbing();
            } 
        }

        // Movement
        if (Mathf.Abs(moveX) > 0.01f) rb.velocity = new Vector2(Mathf.Clamp(speed * moveX, -speed, speed), rb.velocity.y);
        if (climbing && Mathf.Abs(moveY) > 0.01f) rb.velocity = new Vector2(0, Mathf.Clamp(walkSpeed * moveY, -walkSpeed, walkSpeed));

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded || climbing)
            {
                jump();
            }
            else if (canDoubleJump && !doubleJumped)
            {
                doubleJumped = true;
                jump();
            }
        }

        //Crouching
        if (Input.GetKey(KeyCode.S))
        {
            if (!crouching && !climbing) crouch();
        }
        else if(crouching)
        {
            List<Collider2D> cr = new List<Collider2D>();
            cc.OverlapCollider(new ContactFilter2D(), cr);
            if (cr.Count == 0) uncrouch();
        }

        //Reset Double Jump on Ground/Climb
        if (doubleJumped && (grounded || climbing)) doubleJumped = false;

        //Full stop when really slow
        if (rb.velocity.magnitude < 0.3f) rb.velocity = Vector2.zero;
        //Debug.Log("Speed: " + rb.velocity.magnitude);
    }

    private Vector3 getLadderRaycastOrigin(bool reverse = false)
    {
        float xOff = facingRight ? 0.42f : -0.42f;
        if (reverse) xOff *= -1;
        return new Vector3(transform.position.x + xOff, transform.position.y - 0.8f, transform.position.z);
    }

    private void jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
    }

    private void crouch()
    {
        crouching = true;
        transform.localScale = crouchScale;
        pc.transform.localPosition = new Vector3(0, -.75f, 0);
    }

    private void uncrouch()
    {
        crouching = false;
        transform.localScale = Vector3.one;
        pc.transform.localPosition = Vector3.zero;
    }

    private void startClimbing()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        climbing = true;
    }

    private void stopClimbing()
    {
        rb.gravityScale = 2f;
        climbing = false;
    }
}
