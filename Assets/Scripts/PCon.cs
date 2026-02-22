using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCon : MonoBehaviour
{
	[SerializeField] private Camera _camera;
	[SerializeField] private float v, v4, vb, j, vx, tmr, maxJ, maxJb = 12;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private byte mode, _modecounter;
	//[MenuItem("Controllers")]
	public int LegalControl;
	public Controllers _LegalControl;
	public float _chapterTime = 5f;
	[SerializeField] private Image[] controllers;
	[SerializeField] private Bullet bullet;
	
	//[MenuItem("PlayerCollider")]
	[SerializeField] private float groundRadius = 0.3f;
	[SerializeField] private Transform groundCheck, left, right, CamTake, up;
	[SerializeField] private LayerMask groundMask; 
	[SerializeField] private bool jW, an, jumping, started, _locker; 
	[SerializeField] private Sprite[] jbmv;
	private SpriteRenderer sel;

	[SerializeField] private PlayerAnima anim;
	[SerializeField] private SpriteRenderer sr;

	[SerializeField] private float blu, px0, py0;
	[SerializeField] private Sprite[] cons;

	[SerializeField] private BoxCollider2D[] cols;
	[SerializeField] private byte cls;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		sel = GetComponent<SpriteRenderer>();
		rb.drag = 1;
		// StartCoroutine("ControllerSwitcher");
		SwitchControl();
		// px0 = controllers[0].transform.position.x;
		// py0 = controllers[0].transform.position.y;
	}

	void OnEnable(){
		SwitchControl();
	}

	void Update(){
		if(!started){
			if(Input.GetKey(KeyCode.F2)) {v4 = 5;}
			started = false;
		}
	}

	void FixedUpdate(){
		switch(mode){
			case 0:
				float ax = Input.GetAxis("Horizontal") * v;
				if(ax > 0 && isGrounded(right)) ax = 0;
				else if(ax < 0 && isGrounded(left)) ax = 0;

				if(j < maxJ && Input.GetKey(KeyCode.Space)){
					j += 0.1f;
					if(!jumping && isGrounded(groundCheck)) jumping = true;
				} else if(!jumping) {
					//j -= 0.1f;
					j = 0;
				} else {
					jumping = false;
				}

				rb.velocity = new Vector2(ax, jumping ? j : rb.velocity.y);
				if(rb.velocity.x > 0 && sr.flipX) sr.flipX = false; 
				else if(rb.velocity.x < 0 && !sr.flipX) sr.flipX = true;

				if(jumping){
					anim.SetLimits(11, 12);
				} else if(!isGrounded(groundCheck)) {
					anim.SetLimits(13, 14);
				} else {
					if(Mathf.Abs(rb.velocity.x) < 0.01f){
						anim.SetLimits(7, 10);
					} else {
						anim.SetLimits(0, 6);
					}
				}
				break;
			case 1:
				if(Input.GetKey(KeyCode.A) && tmr <= 0) vx = -vb;
				if(Input.GetKey(KeyCode.D) && tmr <= 0) vx = vb;
				if((isGrounded(left) || isGrounded(right)) && tmr <= 0) {vx *= -1; jW = true; tmr = 0.25f; anim.SetLimits(2, 2); anim.Redraw(); /*sel.sprite = jbmv[1]; anim.SetBool("Move", true);*/}
				vx *= 0.95f;
				rb.velocity = new Vector2(vx, Input.GetKeyDown(KeyCode.Space) && isGrounded(groundCheck) || jW ? maxJb : rb.velocity.y * (isGrounded(up) || isGrounded(groundCheck) ? -0.5f : 1));
				jW = false;
				tmr -= 0.02f; 
				if(!isGrounded(left) && !isGrounded(right) && tmr < -0.2f){anim.SetLimits(0, 1);}

				break;
			case 2:
				rb.velocity = new Vector2(Input.GetAxis("Horizontal") * v4, Input.GetAxis("Vertical") * v4);
				if(Input.GetKey(KeyCode.W)) {anim.SetLimits(4, 7);} else if(Input.GetKey(KeyCode.S)) {anim.SetLimits(0, 3);}
				if(Input.GetKey(KeyCode.D)) {sr.flipX = false; anim.SetLimits(8, 9);} else if(Input.GetKey(KeyCode.A)) {sr.flipX = true; anim.SetLimits(8, 9);}
				break;
			case 3:
				if(blu <= 0 && Input.GetMouseButtonDown(0)) {
					Vector3 mousePos = Input.mousePosition;
					
					//float x = mousePos.x / 1920 * 2 - 1;
					//float y = (1080 - mousePos.y) / 1080 * 2 - 1;
					//x *= 1920/1080;
					//Vector2 rd = new Vector3(x, -y, 0);
					//Vector2 ro = new Vector2(transform.position.x % 14, transform.position.y % 14);
					//bullet.transform.position = transform.position;
					Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
					bullet.gameObject.SetActive(true);
					bullet.toMov = new Vector3(pos.x, pos.y, 0);
					bullet.transform.position = transform.position;
					bullet.counter = 1;
					blu = 2;
					anim.SetLimits(2, 2);


					//rd += ro;
					
					//bulletTaker.GetChild(bullets).transform.position = new Vector2(transform.position.x, transform.position.y) + rd*2;
					//bulletTaker.GetChild(bullets).GetComponent<Rigidbody2D>().velocity = rd*20;
					//bullets += 1;
					//bullets %= 10;
					//print($"{x}, {y}");
				} else {
					blu -= Time.deltaTime;
					anim.SetLimits(0, 1);
				}
				break;

		}
	}

	public void clearAnim(){
		//anim.SetBool("Move", false);
	}

	public bool isGrounded(Transform type) {
		Collider2D col = Physics2D.OverlapCircle(type.position, groundRadius, groundMask);
		if(col == null) return false;
		if(!col.isTrigger) return true;
		return false;
	}

	public void SetFirstPossibleController(){
		StopCoroutine("ControllerSwitcher");
		StartCoroutine("ControllerSwitcher");
		_modecounter = 0;
		mode = (byte) (_LegalControl.able[_modecounter] - 1);
		// mode = 0;
		// //anim.SetBool("Move", false);
		// if((LegalControl & (1 << mode)) == 0){
		// 	while((LegalControl & (1 << mode)) == 0){
		// 		mode += 1;
		// 		mode = (byte) (mode & 3);
		// 		an = false;
		// 	}
		// }
		anim.SetMode(mode);

		if(mode == 2){
			rb.gravityScale = 0;
		} else {
			rb.gravityScale = 1.25f;
		}

		if(mode == 4){
			// player.Lock();
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		} else {
			if(!_locker) Unlock();
		}

		// byte realI = 0;

		// for(byte i = 0; i < 4; i++){
		// 	byte len = (byte) (mode + i); 
		// 	len &= 3;
		// 	if((LegalControl & (1 << len)) != 0){

		// 		controllers[realI].sprite = cons[len];
		// 		controllers[realI].gameObject.SetActive(true);
		// 		// controllers[len].gameObject.SetActive(true);
		// 		// controllers[len].transform.position = new Vector3(px0, py0 - 236* realI, 0);
		// 		realI += 1;
		// 	} else {
		// 		// controllers[len].gameObject.SetActive(false);
		// 		controllers[i].gameObject.SetActive(false);
		// 	}
		// }

		for(byte i = 0; i < 4; ++i) controllers[i].gameObject.SetActive(false);
		for(byte i = 0; i < _LegalControl.able.Length; ++i){
			controllers[i].sprite = cons[_LegalControl.able[i]-1];
			controllers[i].gameObject.SetActive(true);
		}

	}

	IEnumerator ControllerSwitcher(){
		while(true) {
			// print("Works");
			yield return new WaitForSeconds(_chapterTime);
			SwitchControl();

		}
	}

	public void SwitchControl(){
		// print("work");
		blu = 0;
		_modecounter += 1;
		_modecounter = (byte) (_modecounter % _LegalControl.able.Length);
		mode = (byte) (_LegalControl.able[_modecounter] - 1);
		// mode += 1;
		// //anim.SetBool("Move", false);
		// mode = (byte) (mode & 3);
		// if((LegalControl & (1 << mode)) == 0){
		// 	while((LegalControl & (1 << mode)) == 0){
		// 		mode += 1;
		// 		mode = (byte) (mode & 3);
		// 		an = false;
		// 	}
		// }



		anim.SetMode(mode);
		cols[cls].enabled = false;
		cols[mode].enabled = true;
		cls = mode;
		if(mode == 2){
			rb.gravityScale = 0;
		} else {
			rb.gravityScale = 1.25f;
		}
		vx = 0;
		tmr = 0;

		if(mode == 4){
			// player.Lock();
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		} else {
			if(!_locker) Unlock();
		}

		// byte realI = 0;

		// for(byte i = 0; i < 4; i++){
		// 	byte len = (byte) (mode + i); 
		// 	len &= 3;
		// 	if((LegalControl & (1 << len)) != 0){
		// 		// controllers[len].gameObject.SetActive(true);
		// 		controllers[realI].sprite = cons[len];
		// 		controllers[realI].gameObject.SetActive(true);
		// 		// controllers[len].transform.position = new Vector3(20,  -130 - 213*realI, 0);
		// 		realI += 1;
		// 	} else {
		// 		controllers[i].gameObject.SetActive(false);
		// 	}
		// }

		for(byte i = 0; i < _LegalControl.able.Length; ++i){
			controllers[i].sprite = cons[_LegalControl.able[(i+_modecounter)%_LegalControl.able.Length]-1];
			// controllers[i].gameObject.SetActive(true);
		}
	}

	public void SkipControl(){
		SwitchControl();
		StopCoroutine("ControllerSwitcher");
		StartCoroutine("ControllerSwitcher");
	}

	public void Lock(){
		// rb.constraints.freezePosition = true;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		_locker = true;
	}

	public void Unlock(){
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		_locker = false;
		// rb.constraints.freezePositionY = false;
	}
}
