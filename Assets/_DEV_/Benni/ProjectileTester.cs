﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}


    void StartBomb()
    {
        const float BOMB_RADIUS = 50;
        const float BOMB_FORCE = 250;

        Collider[] Colliders = Physics.OverlapSphere(this.transform.position, BOMB_RADIUS);   
        foreach (Collider hit in Colliders)
        {
            if (!hit) continue;
            
            IForceReceivable interfaceR = InterfaceUtility.GetInterface<IForceReceivable>(hit.gameObject);
            if (interfaceR == null) continue;
            interfaceR.ReceiveForce(this.gameObject, BOMB_FORCE, BOMB_RADIUS);
        }
    }
}
