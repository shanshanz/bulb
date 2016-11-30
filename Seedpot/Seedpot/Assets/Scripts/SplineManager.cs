using UnityEngine;
using System.Collections;

public class SplineManager : MonoBehaviour {
	
	public Vector3[] points;
	public GameObject[] objects;
	public GameObject objCutter; //selected in the editor
	public int pointsCount;

	public float spacing;
	int currPointInd;

	void Start()
	{
		pointsCount = 3;
		points = new Vector3[pointsCount];
		objects = new GameObject[pointsCount];
		spacing = 2;
	}

	void Update()
	{
		mousePressed ();
		drawLine ();
	}

	void mousePressed()
	{
		if (Input.GetMouseButtonDown (0)) {
//			mousePosition ();
			newObj (newPoint ());
		}
	}
	void mousePosition()
	{
		Debug.Log ("Mouse clikced!");
		float mouseX = Input.mousePosition.x;
		float mouseY = Screen.height - Input.mousePosition.y;
		Debug.Log ("New click at: x " + mouseX + ", y " + mouseY);
	}

	Vector3 newPoint()
	{
		currPointInd++;

		if(currPointInd>= pointsCount)
			currPointInd = 0;
		
		Vector3 point = new Vector3 (objCutter.transform.position.x, objCutter.transform.position.y+ spacing*currPointInd, objCutter.transform.position.z);
		points[currPointInd]=point;
		Debug.Log ("New point made");

		return point;
	}

	void newObj(Vector3 point)
	{
		if (objects [currPointInd] == null) {
			
			Debug.Log ("New obj at: x " + point.x + ", y " + point.y);
			GameObject obj = Instantiate (objCutter, point,Quaternion.identity) as GameObject;
			obj.transform.Rotate(Vector3.up * currPointInd*30);
			objects [currPointInd] = obj;
		}

	}

	void drawLine()
	{
		
	}

	void drawObjects()
	{
		for (int i = 0; i < pointsCount; i++)
		{
			GameObject obj = Instantiate(objCutter, new Vector3((float)i, 1, 0), Quaternion.identity) as GameObject;
			obj.transform.localScale = Vector3.one;
			objects[i] = obj;
		}
	}
	void mouseDetectRay()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log ("Mouse clikced!");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Vector3 point = hit.point;
				/*
				points[nextPointInd % 3]=point;
				nextPointInd++;
				*/

				Debug.Log ("New point made at " + point.x + point.y + point.z);
			}
		}
	}

	/*
	public Vector3 GetPoint (float t) {
		return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
	}

	public Vector3 GetVelocity (float t) {
		return transform.TransformPoint(
			Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
	}

	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public void Reset () {
		points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
	}
	*/
}