using UnityEngine;
using System.Collections;

public class pickUpAndThrow : MonoBehaviour {
	public Camera CameraFacing;
	Transform hitObject;
	public bool objectGrabbed = false;
	public float throwforce = 10.0f;
	public float pullspeed = 8.0f;
	public float pointdistance = 3.0f;

	// Update is called once per frame
	void Update () {
		Vector3 forward = CameraFacing.transform.TransformDirection(Vector3.forward) * 2;
		Debug.DrawRay(transform.position, forward, Color.green);
		if(objectGrabbed) {
			Vector3 targetpoint = CameraFacing.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
			targetpoint += CameraFacing.transform.forward * pointdistance;
			Vector3 force = targetpoint - hitObject.transform.position;
			
			hitObject.GetComponent<Rigidbody>().velocity = force.normalized * hitObject.GetComponent<Rigidbody>().velocity.magnitude;
			hitObject.GetComponent<Rigidbody>().AddForce(force * pullspeed);
			
			hitObject.GetComponent<Rigidbody>().velocity *= Mathf.Min(1.0f, force.magnitude / 2);
		}
		if(!objectGrabbed) {
			RaycastHit hit;
			if (Physics.Raycast(new Ray(CameraFacing.transform.position, CameraFacing.transform.rotation * Vector3.forward * 2.0f), out hit)) {
				if(hit.distance < 3) {
					if(hit.rigidbody) {
						if(hit.transform.CompareTag("Grabbable")) {
							hitObject = hit.transform;
							if(Input.GetButtonDown("Fire1")) {
								hitObject.GetComponent<Rigidbody>().useGravity=false;
								objectGrabbed = true;
							}
						}
					}
				}
			}
		}
		else {
			if(Input.GetButtonDown("Fire1"))
			{
				hitObject.GetComponent<Rigidbody>().velocity=CameraFacing.transform.forward * throwforce;
				objectGrabbed = false;
				hitObject.GetComponent<Rigidbody>().useGravity=true;
			}
		}
	}
}
