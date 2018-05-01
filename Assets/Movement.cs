using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    public float movementSpeed = 15.0f;
    public float counterClockwise = -2.0f;
	Player player;
    // Use this for initialization
    void Start () {
		Player player = new Player ();
		Scene scene = SceneManager.GetActiveScene ();
		if (scene.name == "Level 1") {
			player.tools = 4;
		} else if (scene.name == "Level 2") {
			player.tools = 10;
		}
	 

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.forward * Time.deltaTime * movementSpeed, Space.World);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-transform.forward * Time.deltaTime * movementSpeed , Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, Time.deltaTime - counterClockwise, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, Time.deltaTime + counterClockwise, 0);
        }
/*		if (this.collider) {
			this.transform.position=Vector3.zero;

		}*/
		Vector3 currDirection = this.transform.position;

    }
	void OnTriggerEnter(){
		print ("triggered"+ gameObject.name);
	}
    void OnCollisionEnter(Collision collision)
    {
		//this.transform.position=Vector3.zero;

       print("Collision with Object name: " + collision.gameObject);
    }
}
