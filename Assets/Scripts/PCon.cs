using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCon : MonoBehaviour
{
	[SerializeField] private float v, j, vx, tmr;
	private Rigidbody2D rb;
	[SerializeField] private byte mode;
	//[MenuItem("Controllers")]
	public int LegalControl;
	[SerializeField] private Image[] controllers;
	[SerializeField] private Transform bulletTaker;
	[SerializeField] private int bullets;
	
	//[MenuItem("PlayerCollider")]
	[SerializeField] private float groundRadius = 0.3f;
	[SerializeField] private Transform groundCheck, left, right, CamTake, up;
	[SerializeField] private LayerMask groundMask; 
	[SerializeField] private bool jW, an; 

	[SerializeField] private Animator anim;
	[SerializeField] private SpriteRenderer sr;


	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		rb.drag = 1;
		StartCoroutine("ControllerSwitcher");
		SwitchControl();
	}

	void FixedUpdate(){
		switch(mode){
			case 0:
				float ax = Input.GetAxis("Horizontal") * v;
				if(ax > 0 && isGrounded(right)) ax = 0;
				else if(ax < 0 && isGrounded(left)) ax = 0;
				rb.velocity = new Vector2(ax, isGrounded(groundCheck) ? j * Input.GetAxis("Vertical") : rb.velocity.y);

				if(rb.velocity == Vector2.zero){
					if(an){
						an = !an;
						anim.SetBool("Move", false);
					}
				} else {
					if(!an){
						an = !an;
						anim.SetBool("Move", true);
					}
					if(rb.velocity.x > 0 && sr.flipX) sr.flipX = false; 
					else if(rb.velocity.x < 0 && !sr.flipX) sr.flipX = true;
				}
				break;
			case 1:
				if(Input.GetKey(KeyCode.A) && tmr <= 0) vx = -12.5f;
				if(Input.GetKey(KeyCode.D) && tmr <= 0) vx = 12.5f;
				if((isGrounded(left) || isGrounded(right)) && tmr <= 0) {vx *= -1; jW = true; tmr = 0.25f;}
				vx *= 0.95f;
				rb.velocity = new Vector2(vx, Input.GetKeyDown(KeyCode.W) && isGrounded(groundCheck) || jW ? j : rb.velocity.y * (isGrounded(up) || isGrounded(groundCheck) ? -0.5f : 1));
				jW = false;
				tmr -= 0.02f; 
				break;
			case 2:
				rb.velocity = new Vector2(Input.GetAxis("Horizontal") * v, Input.GetAxis("Vertical") * v);
				break;
			case 3:
				if(Input.GetMouseButtonDown(0)) {
					Vector3 mousePos = Input.mousePosition;
					
					float x = mousePos.x / 1920 * 2 - 1;
					float y = (1080 - mousePos.y) / 1080 * 2 - 1;
					x *= 1920/1080;
					Vector2 rd = new Vector3(x, -y, 0);
					//Vector2 ro = transform.position - CamTake.position;
					//rd += ro;
					
					bulletTaker.GetChild(bullets).transform.position = new Vector2(transform.position.x, transform.position.y) + rd*2;
					bulletTaker.GetChild(bullets).GetComponent<Rigidbody2D>().velocity = rd*20;
					bullets += 1;
					bullets %= 10;
					//print($"{x}, {y}");
				}
				break;

		}
	}

	public bool isGrounded(Transform type) {
		return Physics2D.OverlapCircle(type.position, groundRadius, groundMask);
	}

	IEnumerator ControllerSwitcher(){
		while(true) {
			yield return new WaitForSeconds(10f);
			SwitchControl();
		}
	}

	public void SwitchControl(){
		mode += 1;
		mode = (byte) (mode & 3);
		while((LegalControl & (1 << mode)) == 0){
			mode += 1;
			mode = (byte) (mode & 3);
		}

		if(mode == 2){
			rb.gravityScale = 0;
		} else {
			rb.gravityScale = 1;
		}

		byte realI = 0;

		for(byte i = 0; i < 4; i++){
			byte len = (byte) (mode + i); 
			len &= 3;
			if((LegalControl & (1 << len)) != 0){
				controllers[len].gameObject.SetActive(true);
				controllers[len].transform.position = new Vector3(1637.5f, 844 - 236* realI, 0);
				realI += 1;
			} else {
				controllers[len].gameObject.SetActive(false);
			}
		}
	}
}