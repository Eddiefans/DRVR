using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

	[SerializeField]float destroyTime = 2;

	void OnEnable(){
		Destroy (gameObject, destroyTime);
	}
}
