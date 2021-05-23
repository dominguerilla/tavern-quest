using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    public InputActionAsset controls;

    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float jumpHeight = 10.0f;
    public float gravity = 9.8f;


    CharacterController cha;
    bool isJumping = false;
    float currentHeight = 0f;
    float increment = 0f;
    float vertSpeed = 0f;

    Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Awake () {
        cha = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //moveDirection.y = CalculateVertical();
        CalculateXY(controls["Move"].ReadValue<Vector2>());
    }
    
    public void CalculateXY(Vector2 inputDir){
        Vector3 currentMoveDir = new Vector3(inputDir.x, 0, inputDir.y);

        currentMoveDir = transform.TransformDirection(currentMoveDir);
        currentMoveDir *= speed ;
        this.moveDirection = currentMoveDir;

        cha.Move(moveDirection *= Time.deltaTime);

    }

    float CalculateVertical(){
        if(cha.isGrounded) {
            vertSpeed = 0f;
            if (Input.GetButtonDown("Jump")) {
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
