using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour {

	public float speed = 1; // make it a variable later
	public Transform Target;
	public Camera cam;

	// Update is called once per frame
	void LateUpdate () {
		Move();
	}

	public void Move(){

		Vector3 direction = (Target.position - cam.transform.position).normalized;
		Quaternion lookrotation = Quaternion.LookRotation(direction);
		//rotate in y only
		lookrotation.x = transform.rotation.x;
		lookrotation.z = transform.rotation.z;
		//assign a rotation (control speed in last part)
		transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime*100);
		transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime * speed);
		
	}
}
