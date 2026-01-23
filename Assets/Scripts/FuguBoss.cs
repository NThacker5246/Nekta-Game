using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuguBoss : MonoBehaviour
{
	[SerializeField] private Transform up, down, left, right;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Location locall;
    [SerializeField] private Vector3 inital;
    // Start is called before the first frame update

    void Awake(){
        inital = transform.position;
    }

    void OnEnable()
    {
        transform.position = inital;
        rb.velocity = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        bool u = Physics2D.OverlapCircle(up.position, 0.025f, groundMask);
    	bool d = Physics2D.OverlapCircle(down.position, 0.025f, groundMask);
    	bool l = Physics2D.OverlapCircle(left.position, 0.025f, groundMask);
    	bool r = Physics2D.OverlapCircle(right.position, 0.025f, groundMask);

		if(u){
			rb.velocity = new Vector2(rb.velocity.x, -Random.Range(0.5f, 1.25f));
		} else if(d){
			rb.velocity = new Vector2(rb.velocity.x, Random.Range(0.5f, 1.25f));
		}
		if(l){
			rb.velocity = new Vector2(Random.Range(0.5f, 1.25f), rb.velocity.y);
		} else if(r){
			rb.velocity = new Vector2(-Random.Range(0.5f, 1.25f), rb.velocity.y);
		}
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Player"){
    		//kill
    		locall.RestartLevel();
    	} else if(other.tag == "floor") {
    		// bool u = Physics2D.OverlapCircle(up.position, 0.5f, groundMask);
    		// bool d = Physics2D.OverlapCircle(down.position, 0.5f, groundMask);
    		// bool l = Physics2D.OverlapCircle(left.position, 0.5f, groundMask);
    		// bool r = Physics2D.OverlapCircle(right.position, 0.5f, groundMask);

    		// if(u){
    		// 	rb.velocity = new Vector2(rb.velocity.x, 1);
    		// }
    		// if(d){
    		// 	rb.velocity = new Vector2(rb.velocity.x, -1);
    		// }
    		// if(l){
    		// 	rb.velocity = new Vector2(-1, rb.velocity.y);
    		// }
    		// if(r){
    		// 	rb.velocity = new Vector2(1, rb.velocity.y);
    		// }

    	} else if(other.tag == "Bullet"){
            rb.velocity = Vector3.Normalize(other.GetComponent<Rigidbody2D>().velocity) * rb.velocity.magnitude;
            other.gameObject.SetActive(false);
        }
    }
}
