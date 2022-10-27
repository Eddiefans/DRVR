using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneRestart : MonoBehaviour {

	public void restart () {

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}