using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 20.0f;
    public float minDist = 1f;
    Transform target;
    public string Target;
    //public Rigidbody rb;

    // Use this for initialization
    private void Start()
    {
        //rb= gameObject.GetComponent<Rigidbody>();
        // if no target specified, assume the player
        if (target == null)
        {
            if (GameObject.FindWithTag(Target) != null)
            {
                target = GameObject.FindWithTag(Target).GetComponent<Transform>();
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        FollowPlayer();
    }

    // Set the target of the chaser
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void FollowPlayer()
    {
        if (target == null)
            return;
        // face the target
        transform.LookAt(target); 
        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);
        //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
        if (distance > minDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else {
            transform.position += transform.forward * speed * Time.deltaTime;
            Destroy(this.gameObject);
        }
    }

    void FollowPlayer2()
    {
        transform.LookAt(target);
        //I will create a vector 3 called pos that stores the movement that I want my player to do
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //I will use these two built-in functions to follow the player
        //rb.MovePosition(pos);
    }
}