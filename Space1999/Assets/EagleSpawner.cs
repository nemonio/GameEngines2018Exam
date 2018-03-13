using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour {

	public float gap = 20;
	public float followers = 4;
	public GameObject prefab;

	void Awake () {

		for (int row = 0; row <= followers; row++) {

			GameObject myGameObjectInTheLeft = Instantiate (prefab);

			Vector3 leftOffset = new Vector3 (-row * gap, 0, -row * gap);

			myGameObjectInTheLeft.transform.position = transform.position + leftOffset;

			//myGameObjectInTheLeft.transform.rotation = this.transform.rotation;

			myGameObjectInTheLeft.transform.SetParent (this.transform);



			if (row != 0) {
				
				GameObject myGameObjectInTheRight = Instantiate (prefab);

				Vector3 rightOffset = new Vector3 (row * gap, 0, -row * gap);

				myGameObjectInTheRight.transform.position = transform.position + rightOffset;

				//myGameObjectInTheRight.transform.rotation = this.transform.rotation;

				myGameObjectInTheRight.transform.SetParent (this.transform);


				//Change to 
				//SteeringBehaviour followerSeek = myGameObjectInTheLeft.AddComponent<Seek> ();


			} else {

				myGameObjectInTheLeft.gameObject.name = "Leader";

				SteeringBehaviour leaderSeek = myGameObjectInTheLeft.AddComponent<Seek> ();

			}



		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
