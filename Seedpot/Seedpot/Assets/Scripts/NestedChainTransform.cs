using UnityEngine;
using System.Collections;

public class NestedChainTransform : MonoBehaviour {
	
	public GameObject objCutter;
	public float spacing = 5f; 
	int nodesNum = 10;
	Vector3 childPos;


	void Start () {
		childPos = new Vector3(0,0,0);
		for (int i = 0; i < nodesNum; i++) {
			createChildren (i);
		}
	}
	void Update () {
		baseMove ();
		chainMove();
	}


	void createChildren(int ind)
	{
		float scaleUp = Mathf.Sin(ind*Mathf.PI/nodesNum)*3f;
		childPos += Vector3.up * scaleUp;
		GameObject obj = Instantiate(objCutter, childPos, Quaternion.identity,gameObject.transform) as GameObject;
		obj.transform.localScale += new Vector3(scaleUp,scaleUp,scaleUp);
//		Debug.Log ("Scaling up: " +scaleUp);
	}


	void baseMove()
	{
//		baseSpin ();
	}

	void baseSpin()
	{
		gameObject.transform.Rotate(Vector3.up*Time.fixedDeltaTime*10);
	}

	void baseSwing()
	{
		gameObject.transform.position += Vector3.left * Mathf.Sin(Time.fixedTime);
	}

	void chainMove()
	{
		int index = 0;
		foreach (Transform child in transform) {
			float wiggle = Mathf.Sin ((Time.frameCount*0.5f+index*3)*0.1f);
			chainSwing (child, wiggle);
			chainSpin (child, wiggle);
			chainBob (child, wiggle);
			index++;
		}

	}

	void chainSwing(Transform child, float wiggle)
	{
		child.position = new Vector3(wiggle,child.position.y,0);
	}

	void chainSpin(Transform child, float wiggle)
	{
		child.Rotate (Vector3.up*wiggle);
	}
	void chainBob(Transform child, float wiggle)
	{
		wiggle*=0.075f;
		child.transform.localScale += new Vector3(wiggle,wiggle,wiggle);
	}

}
