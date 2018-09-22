using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float jumpHeight = 10.0f;
    public float gravity = 9.8f;


    CharacterController cha;
    bool isJumping = false;
    float currentHeight = 0f;
    float increment = 0f;
    float vertSpeed = 0f;

	// Use this for initialization
	void Start () {
        cha = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDirection = CalculateXY();
        moveDirection.y = CalculateVertical();

        cha.Move(moveDirection *= Time.deltaTime);
    }
    
    Vector3 CalculateXY(){
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed ;
        return moveDirection;
    }

    float CalculateVertical(){
        if(cha.isGrounded) {
            vertSpeed = 0f;
            if (Input.GetButtonDown("Jump")) {
                Debug.Log("JUMP");
                isJumping = true;
            }
        }

        // gradually ascend
        if (isJumping){
            increment += jumpSpeed * Time.deltaTime;
            currentHeight = Mathf.Lerp(0, jumpHeight, increment);
            if(currentHeight >= jumpHeight){
                currentHeight = 0f;
                isJumping = false;
                increment = 0f;
            }else{
                vertSpeed = currentHeight;
            }
        }else{
            vertSpeed -= gravity * Time.deltaTime;
        }
        return vertSpeed;
    }
}
