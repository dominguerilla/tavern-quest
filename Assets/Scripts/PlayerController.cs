using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 9.8f;

    CharacterController cha;

	// Use this for initialization
	void Start () {
        cha = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        float vSpeed = -1.0f;
        if(cha.isGrounded) {
            if(Input.GetButtonDown("Jump")) {
                vSpeed = jumpSpeed;
            }
        }
        vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = vSpeed;
        cha.Move(moveDirection);
    }
}
