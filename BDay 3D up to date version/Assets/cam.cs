using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour {

    private Rigidbody rb;
    private GameObject temp;
    private Camera mainCam;
    private float camOffset;
    private float thetaView;
    private Quaternion viewOrient;
    
    // Use this for initialization
    void Start () {
        temp = GameObject.FindGameObjectsWithTag("Player")[0];
        rb = temp.GetComponent<Rigidbody>();
        mainCam = GetComponent<Camera>();
        camOffset = -1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Vector3 offset = new Vector3(0, 1, -2);
        //Vector3 playerPos = rb.position+offset;
        //mainCam.transform.position = playerPos;

        thetaView = Mathf.Atan2(rb.velocity.z, rb.velocity.x);

        Vector3 camPos = new Vector3(camOffset *Mathf.Cos(thetaView), 5f, camOffset * Mathf.Sin(thetaView));

        Vector3 playerPos = rb.position+camPos;
        mainCam.transform.position = playerPos;

        float thetaViewDeg = thetaView * Mathf.Rad2Deg;

        viewOrient = Quaternion.Euler(45f, thetaViewDeg, 0f);

        mainCam.transform.rotation = viewOrient;



    }

    private void OnGUI()
    {

        GUI.Label(new Rect(20, 100, 80, 20), thetaView * Mathf.Rad2Deg + "degrees about y");
    }
}
