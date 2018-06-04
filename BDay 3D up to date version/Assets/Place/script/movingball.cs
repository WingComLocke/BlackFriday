using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingball : MonoBehaviour {

    private Rigidbody rb;
    private GameObject temp;
    private Camera mainCam;
    private float thetaView;
    float speed = 30;
    const float maxV = 2;
   
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        temp = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        mainCam = temp.GetComponent<Camera>();
        rb.position = new Vector3(0, 1, 0);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //y axis rotation, angle between the x axis and the directional vector in RAD
        thetaView = Mathf.Atan2(rb.velocity.z, rb.velocity.x);


        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        Vector3 curV= rb.velocity;

        if(curV.magnitude<maxV) rb.AddForce(movement);

        //rb.position.x, rb.position.y + 0.1f, rb.position.z
        //cam.transform.position= new Vector3(0,1,0);
	}
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 200, 100), "Velocity");
        GUI.Label(new Rect(20, 40, 80, 20), rb.velocity.x + "/s in x");
        GUI.Label(new Rect(20, 70, 80, 20), rb.velocity.z + "/s in z");    }
}

