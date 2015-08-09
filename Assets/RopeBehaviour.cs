using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeBehaviour : MonoBehaviour {
	
	public GameObject Bait;
	public GameObject LinkPrefab;
	
	public float linkDistance;
	
	private List<GameObject> linkList;
	
	private float lane;
	
	private bool shooting = false;
	
	public bool shoot = false;
	
	private GameObject lastLink;
	
	private Rigidbody2D rigidbody;
	
	// Use this for initialization
	void Awake () {
		linkList = new List<GameObject>();
		lane = transform.position.y;
		this.rigidbody = GetComponent<Rigidbody2D> ();
		
	}
	
	 

	void Start()
	{
		this.Shoot(Vector2.zero,0);

	}
	void FixedUpdate () {
		 
		if(shooting){
			 if(Bait.GetComponent<Rigidbody2D>().position.y <= lane){
				shooting = false;
				Bait.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				Bait.GetComponent<Rigidbody2D>().gravityScale = 0;
				Bait.AddComponent<HingeJoint2D>();
				Bait.GetComponent<HingeJoint2D>().connectedBody = lastLink.GetComponent<Rigidbody2D>();
				
				foreach ( GameObject link in linkList){
					link.GetComponent<Rigidbody2D>().gravityScale = 0;
				}
				return;
			}
			 
			Vector2 midPosition;
			Vector2 currentDistance;
			if(linkList.Count == 0){
				lastLink = gameObject;
				Debug.Log(Bait.GetComponent<Rigidbody2D>().position);
				midPosition =  (new Vector2(Bait.transform.position.x,Bait.transform.position.y) + new Vector2(transform.position.x,transform.position.y))/2;
				currentDistance = new Vector2(transform.position.x,transform.position.y);
			}else{
				lastLink = linkList[linkList.Count-1];
				midPosition = (new Vector2(Bait.transform.position.x,Bait.transform.position.y) + new Vector2(lastLink.transform.position.x,lastLink.transform.position.y))/2;
				currentDistance = new Vector2(lastLink.transform.position.x,lastLink.transform.position.y);
			}
			//Debug.Log(midPosition);ew
			
			if(Vector2.Distance(Bait.GetComponent<Rigidbody2D>().position,currentDistance)>=linkDistance){
				print("ok"+midPosition);
				GameObject newLink = (GameObject) Instantiate(LinkPrefab,midPosition,Quaternion.identity);

				print ("no"+newLink.transform.position);
				newLink.GetComponent<HingeJoint2D>().connectedBody = lastLink.GetComponent<Rigidbody2D>();
				linkList.Add(newLink);
			}
		}
		
	}
	
	void Shoot(Vector2 force, float lane){
		Bait.GetComponent<Rigidbody2D>().gravityScale = 1;
		Bait.GetComponent<Rigidbody2D>().AddForce (new Vector2(-1,1)*300);
		shooting = true;
		shoot = false;
		
	}
	void Pull(){
	}
	void Hooked(){
	}
	void GetBait(){
		
	}
	
