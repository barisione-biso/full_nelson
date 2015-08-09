using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeBehaviour : MonoBehaviour {

	public Transform Bait;
	public GameObject LinkPrefab;

	public float linkDistance;

	private List<GameObject> linkList;

	// Use this for initialization
	void Start () {
		linkList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 midPosition;
		Vector3 currentDistance;
		if(linkList.Count == 0){
			midPosition =  Bait.position - transform.position;
			currentDistance = transform.position;
		}else{
			GameObject lastLink = linkList[linkList.Count-1];
			midPosition = Bait.position - lastLink.transform.position;
			currentDistance = lastLink.transform.position;
		}
		Debug.Log(Vector3.Distance(currentDistance,Bait.position));

		if(Vector3.Distance(Bait.position,currentDistance)>=linkDistance){
			GameObject newLink = (GameObject) Instantiate(LinkPrefab,midPosition, transform.rotation);
			linkList.Add(newLink);
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
