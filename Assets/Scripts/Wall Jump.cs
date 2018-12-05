using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {
    public float distance = 1f;
    SimplePlatformController movement ;

    // Use this for initialization
    void Start () {
        movement = GetComponent<SimplePlatformController>();

    }
	
	// Update is called once per frame
	void Update () {

        RaycastHir2D hit = Phyisics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        if (Input.GetButtonDown("Jump") && !movemrnt.grounded)
        {

        }

    }
}
