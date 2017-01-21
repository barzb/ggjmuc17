using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                this.transform.position = hit.point;
                StartBomb();
            }
        }
	}


    void StartBomb()
    {
        const float BOMB_RADIUS = 50;
        const float BOMB_FORCE = 100;

        Collider[] Colliders = Physics.OverlapSphere(this.transform.position, BOMB_RADIUS);   
        foreach (Collider hit in Colliders)
        {
            if (!hit) continue;
            
            IForceReceivable interfaceR = InterfaceUtility.GetInterface<IForceReceivable>(hit.gameObject);
            interfaceR.ReceiveForce(this.gameObject, BOMB_FORCE, BOMB_RADIUS);
        }
    }
}
