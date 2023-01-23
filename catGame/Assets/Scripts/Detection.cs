using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour
{

    public GameObject plyr; //this specifically helps the canseeplayer proc
    public GameObject[] targets; //targets for rays casted will go here.
    public float coneFov = 120f;
    public float coneDist = 6f; //this is the distance of the front cone.
    public float radDist = 1.75f; //this is the distance of the back  detection.
    RaycastHit hitInfo; //object hit by the ray.
    public bool seeyou = false;


    ///////Solution2 drag head object into public variable to set this up
    public GameObject head;
    /////////////////

    //my procs
    protected bool CanSeePlayer() //this procedure detects in front of the character
    {
        targets = GameObject.FindGameObjectsWithTag("tgt");
        plyr = GameObject.FindGameObjectWithTag("Player");
        RaycastHit hit;
        
        foreach (GameObject currentTarget in targets)
        {
            Vector3 rayDirection = currentTarget.transform.position - head.transform.position;
            if ((Vector3.Angle(rayDirection, transform.forward)) <= coneFov * 0.5f)//this detects if player is within the cone of view.
            {
                // Detect if player is within the range of view. find out if we can flip the above statement and this one.
                if (Physics.Raycast(head.transform.position, rayDirection, out hit, coneDist))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        return (hit.transform.CompareTag("Player"));
                    }
                }
            }
        }

        return false;
    }

    protected bool CanFeelPlayer() //this procedure detects around the character for a very short distance so player can't get too close.
        //Maybe this can be a single ray shot at player's transform center.
    {
        targets = GameObject.FindGameObjectsWithTag("tgt");
        plyr = GameObject.FindGameObjectWithTag("Player");
        RaycastHit hit;

        foreach (GameObject currentTarget in targets)
        {
            Vector3 rayDirection = currentTarget.transform.position - head.transform.position;
            if ((Vector3.Angle(rayDirection, head.transform.forward)) >= coneFov * .5f)
            {
                // Detect if player is within the field of view
                if (Physics.Raycast(head.transform.position, rayDirection, out hit, radDist))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        return (hit.transform.CompareTag("Player"));
                    }
                }
            }
        }
        return false;
    }
    //game procs

    void Update()
    {
        if (CanSeePlayer())
        {
            seeyou = true;
        }
        if (CanFeelPlayer())
        {
            seeyou = true;
        }
    }
}

