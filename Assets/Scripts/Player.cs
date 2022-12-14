using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public Transform playerTrans;

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("Walking");
            playerAnim.ResetTrigger("Idle");
            walking = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("Walking");
            playerAnim.SetTrigger("Idle");
            walking = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("Running Backward");
            playerAnim.ResetTrigger("Idle");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("Running Backward");
            playerAnim.SetTrigger("Idle");
        }

        if(Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }

        if(walking == true)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("Running");
                playerAnim.ResetTrigger("Walking");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed;
                playerAnim.ResetTrigger("Running");
                playerAnim.SetTrigger("Walking");
            }
        }
    }
}
