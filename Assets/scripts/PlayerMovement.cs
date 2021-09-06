using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    float hmove = 0;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        hmove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(hmove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("Crouch", isCrouching);
    }

    //used for physics
    private void FixedUpdate() 
    {
        //time.fixedDeltaTime ensures we move the same amount regardless of how often the function is called
        controller.Move(hmove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
}
