using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableProjectile : MonoBehaviour {

    public float speed;
    private Vector3 movingDirection;
    private float movingInDirectionTimer;

	// Use this for initialization
	void Start () {
        movingDirection = new Vector3(Random.Range(-1f, 1f) , 0f, Random.Range(-1f,1f));
        Debug.Log(movingDirection);
	}
	
	// Update is called once per frame
	void Update () {

        if (movingInDirectionTimer < 3f) {

            Vector3 Velocity = movingDirection;
            GetComponent<Rigidbody>().velocity = (Velocity * Time.deltaTime * speed * 100f);
            movingInDirectionTimer += 1 * Time.deltaTime;
        }
        else
        {
            // set diffrent moving direction and set timer to 0
            movingDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Debug.Log(movingDirection);
            movingInDirectionTimer = 0f;


        }
	}

    
}
