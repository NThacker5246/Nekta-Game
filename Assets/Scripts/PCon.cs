using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCon : MonoBehaviour
{
    [SerializeField] private float v;
    private Rigidbody2D rb;
    [SerializeField] private bool isWallCol;
    [SerializeField] private byte mode;
    [SerializeField] private float j;

    public bool[] LegalControl;

    [SerializeField] private GameObject bullet;
    [SerializeField] private PhysicsMaterial2D BJ;
    [SerializeField] private PhysicsMaterial2D NJ;

    private float groundRadius = 0.3f;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundMask; 


    void Awake(){
    	rb = GetComponent<Rigidbody2D>();
    	rb.drag = 1;
    	StartCoroutine("ControllerSwitcher");
    }

    void FixedUpdate(){

    	if(mode == 0){
    		if(Input.GetKey(KeyCode.D)){
			  rb.velocity = new Vector2(v, rb.velocity.y);
			} else {
			  rb.velocity = new Vector2(0, rb.velocity.y);
			}

			if(Input.GetKey(KeyCode.A)){
			  rb.velocity = new Vector2(v * -1, rb.velocity.y);
			}

    		if(Input.GetKeyDown(KeyCode.Space) && isGrounded()){
		    	rb.AddForce(new Vector2(0, j));
	    	}
    	} else if(mode == 1){
    		if(Input.GetKey(KeyCode.D)){
			  rb.velocity = new Vector2(v, rb.velocity.y);
			} else {
			  rb.velocity = new Vector2(0, rb.velocity.y);
			}

			if(Input.GetKey(KeyCode.A)){
			  rb.velocity = new Vector2(-1 * v, rb.velocity.y);
			}

    		if(Input.GetKeyDown(KeyCode.Space) && isGrounded()){
		    	rb.AddForce(new Vector2(0, j));
	    	}
    	} else if(mode == 2){
    		if(Input.GetKey(KeyCode.D)){
			  rb.velocity = new Vector2(v, rb.velocity.y);
			} else {
			  rb.velocity = new Vector2(0, rb.velocity.y);
			}

			if(Input.GetKey(KeyCode.A)){
			  rb.velocity = new Vector2(v * -1, rb.velocity.y);
			}

			if(Input.GetKey(KeyCode.W)){
			  rb.velocity = new Vector2(rb.velocity.x, v);
			} else {
			  rb.velocity = new Vector2(rb.velocity.x, 0);
			}

			if(Input.GetKey(KeyCode.S)){
			  rb.velocity = new Vector2(rb.velocity.x, v * -1);
			}


    	} else if(mode == 3){
    		if(Input.GetMouseButtonDown(0)) {
	    		Vector3 mousePos = Input.mousePosition;
                
	    		float x = mousePos.x / 1920 * 2 - 1;
	    		float y = mousePos.y / 1080 * 2 - 1;
	    		x *= 1920/1080;
	    		Vector3 rd = new Vector3(x, y, 0);
	    		Vector3 ro = transform.position;
	    		rd = ro + rd*40;
	    		

                GameObject gm = Instantiate(bullet, transform.position, Quaternion.identity);
                gm.GetComponent<Bullet>().toMov = rd;
                gm.GetComponent<Bullet>().player = GetComponent<KeyMan>();
	    	}
		}
    }

	public bool isGrounded() {    
	    return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
	}

    IEnumerator ControllerSwitcher(){
    	while(true) {
	    	yield return new WaitForSeconds(10f);
	    	SwitchControl();
	    }
    }

    public void SwitchControl(){
    	mode += 1;
        mode = (byte) (mode % 4);
        while(!LegalControl[mode]){
            mode += 1;
            mode = (byte) (mode % 4);
        }
    	if(mode == 2){
    		rb.gravityScale = 0;
    	} else {
    		rb.gravityScale = 1;
    	}

        if(mode == 1){
            rb.sharedMaterial = BJ;
        } else {
            rb.sharedMaterial = NJ;
        }
    }
}