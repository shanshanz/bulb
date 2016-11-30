using UnityEngine;
using System.Collections;

public class seedChainLiving : MonoBehaviour
{

	public GameObject objCutter;
	public float spacing = 5f;
	int nodesNum = 10;
	Vector3 childPos;

	float wiggle;
	float prevWiggle;

	int index = 0;

	void Start ()
	{
		childPos = new Vector3 (0, 0, 0);
		for (int i = 0; i < nodesNum; i++) {
			createChildren (i);
		}
	}

	void Update ()
	{
//		baseMove ();
		chainMove ();
	}


	void createChildren (int ind)
	{
		float scaleUp = Mathf.Sin (ind * Mathf.PI / nodesNum) * 3f;
		childPos += Vector3.up * scaleUp;
		GameObject obj = Instantiate (objCutter, childPos, Quaternion.identity, gameObject.transform) as GameObject;
		obj.transform.localScale += new Vector3 (scaleUp, scaleUp, scaleUp);
		//		Debug.Log ("Scaling up: " +scaleUp);
	}


	void baseMove ()
	{
//		baseSpin ();
		baseSwing ();
	}

	void baseSpin ()
	{
		gameObject.transform.Rotate (Vector3.up * Time.fixedDeltaTime);
	}

	void baseSwing ()
	{
//		gameObject.transform.Translate (Mathf.Sin (Time.frameCount * 0.01f),0,0);
		gameObject.transform.Translate(Mathf.Cos (Time.frameCount*0.2f)*0.6f,0,0);
	}

	void chainMove ()
	{
		foreach (Transform child in transform) {
			
			wiggle = Mathf.Sin((index + Time.frameCount)*0.5f);

			chainSwing (child, wiggle);
			chainSpin (child, wiggle);
			chainBob (child, wiggle);
			index++;
		}

	}

	void chainSwing (Transform child, float wiggle)
	{
		child.transform.localPosition = new Vector3(wiggle*-0.5f,child.transform.localPosition.y, 0);
//		child.Translate(wiggle*0.2f,0,wiggle*-0.2f);
	}

	void chainSpin (Transform child, float wiggle)
	{
		child.Rotate (Vector3.up * wiggle*2f);
	}

	void chainBob (Transform child, float wiggle)
	{
		wiggle *= 0.25f;
		child.transform.localScale += new Vector3 (wiggle, wiggle, wiggle);
	}
}
