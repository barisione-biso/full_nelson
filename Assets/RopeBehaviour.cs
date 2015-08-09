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

	// Use this for initialization
	void Start () {
		linkList = new List<GameObject>();
		lane = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		if(shooting){
			if(Bait.GetComponent<Rigidbody>().position.y <= lane){
				shooting = false;
				Bait.GetComponent<Rigidbody>().velocity = Vector3.zero;
				Bait.GetComponent<Rigidbody>().useGravity = false;
				Bait.AddComponent<ConfigurableJoint>();
				Bait.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
				Bait.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
				Bait.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
				Bait.GetComponent<ConfigurableJoint>().connectedBody = lastLink.GetComponent<Rigidbody>();

				foreach ( GameObject link in linkList){
					link.GetComponent<Rigidbody>().useGravity=true;
				}
				return;
			}
			Vector3 midPosition;
			Vector3 currentDistance;
			if(linkList.Count == 0){
				lastLink = gameObject;
				midPosition =  (Bait.GetComponent<Rigidbody>().position + transform.position)/2;
				currentDistance = transform.position;
			}else{
				lastLink = linkList[linkList.Count-1];
				midPosition = (Bait.GetComponent<Rigidbody>().position + lastLink.transform.position)/2;
				currentDistance = lastLink.transform.position;
			}
			//Debug.Log((currentDistance - Bait.position));
			
			if(Vector3.Distance(Bait.GetComponent<Rigidbody>().position,currentDistance)>=linkDistance){
				GameObject newLink = (GameObject) Instantiate(LinkPrefab,midPosition, transform.rotation);
				newLink.GetComponent<ConfigurableJoint>().connectedBody = lastLink.GetComponent<Rigidbody>();
				linkList.Add(newLink);
			}
		}

	}
	void Update () {
		if(shoot){
			Bait.GetComponent<Rigidbody>().useGravity = true;
			Bait.GetComponent<Rigidbody>().AddForce (new Vector3(-1,1,0)*450);
			shooting = true;
			shoot = false;

		}

	}

	void Shoot(Vector2 force, float lane){

	}
	void Pull(){
	}
	void Hooked(){
	}
	void GetBait(){

	}

}
