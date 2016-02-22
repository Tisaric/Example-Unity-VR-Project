using UnityEngine;
using System.Collections;

public class changeGrabbableColor : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Grabbable") {
			other.GetComponent<Renderer>().material.color = Color.green;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "Grabbable") {
			other.GetComponent<Renderer>().material.color = Color.white;
		}
	}
}
