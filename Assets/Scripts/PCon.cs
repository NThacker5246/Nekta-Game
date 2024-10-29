using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCon : MonoBehaviour
{
    [SerializeField] private float a;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isWallCol;
    [SerializeField] private byte mode;
    [SerializeField] private float j;

    public bool[] LegalControl;

    [SerializeField] private GameObject bullet;
    [SerializeField] private PhysicsMaterial2D BJ;
    [SerializeField] private PhysicsMaterial2D NJ;


    void Start(){
    	rb = GetComponent<Rigidbody2D>();
    	StartCoroutine("ControllerSwitcher");
    }

    void FixedUpdate(){
    	float Horizontal = Input.GetAxis("Horizontal")*a;

    	if(mode == 0){
    		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
		    	rb.AddForce(new Vector2(Horizontal, j));
	    	} else {
	    		rb.AddForce(new Vector2(Horizontal, 0));
	    	}
    	} else if(mode == 1){
    		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
		    	rb.AddForce(new Vector2(Horizontal, j));
	    	} else {
	    		rb.AddForce(new Vector2(Horizontal, 0));
	    	}
    	} else if(mode == 2){
    		float Vertical = Input.GetAxis("Vertical")*a;
    		rb.AddForce(new Vector2(Horizontal, Vertical));
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

    void OnTriggerStay2D(Collider2D other){
    	if(other.tag == "floor") {
	    	isGrounded = true;
	    } else if(other.tag == "wall"){
	    	isWallCol = true;
	    }
    }

    void OnTriggerExit2D(Collider2D other){
    	if(other.tag == "floor") {
	    	isGrounded = false;
	    } else if(other.tag == "wall"){
	    	isWallCol = false;
	    }
    }

/*

    public bool isGrounded(){
    	RaycastHit2D hit = new RaycastHit2D();
    	ContactFilter2D filter = new ContactFilter2D();
    	if(Physics2D.Raycast(transform.position, Vector2.down, 1f, 1, 1f)){
    		return true;
    	}
    	return false;
    }

*/
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