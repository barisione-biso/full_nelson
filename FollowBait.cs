using UnityEngine;
using System.Collections;

public class FollowBait : MonoBehaviour {

	public enum TypeOfCarnage{candy ,money,bait3};
	//public TypeOfCarnage fishCarnage;
	public TypeOfCarnage peopleCarnage;
	private Transform target;
	private Transform myTransform;
	public float moveSpeed=1.5f;
	public float range=3;
	public float stopDistance=0.1f;
	private float distance; 
	public bool hooked=false;
	public bool joined=false;

	private DistanceJoint2D dj2d;

	void Awake(){
		myTransform = this.transform;
	}

	void Start () {				

		target = GameObject.FindGameObjectWithTag ("bait").transform;
		//target = GameObject.Find ("Carnage").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (target.name == peopleCarnage.ToString()) {

			distance = Vector2.Distance (myTransform.position, target.position);
	
			if (distance <= range && distance > stopDistance && hooked == false) { 
				//move to the player
				myTransform.position += (target.position - myTransform.position) * moveSpeed * Time.deltaTime;
			} 


			if (distance <= stopDistance && !joined) {    
				hooked = true;
				joined = true;
				// joint to carnage
				dj2d = gameObject.AddComponent<DistanceJoint2D> () as DistanceJoint2D;
				dj2d.connectedBody = target.GetComponent<Rigidbody2D> ();
				dj2d.distance = 0.2f;
			}   
		}
	}
}
