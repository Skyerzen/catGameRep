using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    //variables

    public GameObject player;
    public GameObject GoalObj;

    //Enemy Controller.
    public float gravity = 20.0F; //Gravity multiplier.
    public float reach = .1f; //sets the distance to the waypoint before it changes to the next.
    private Vector3 moveDirection = Vector3.zero; //Character's vector of movement. Set to zero at the very beginning.

    //Enemy Smoothers.
    //movement
    public float changeOfDirection = 4f; //rate of the  character changing from one direction to another.
    public float turnSpeed = 8f; //rate the character whips around when changing direction.

    //waypoints
    public GameObject[] waypointList;
    public float enemySpeed = 3f;
    private int currentWP; //enumerator for waypoints.
    private bool waiting = false;
    private float timer;
    public bool walk = true;

    //my procs.-------------------------------------

    void WaypointDetect() //detects whether the current waypoint about the be reached is a pass or a wait type of checkpoint.
    {
        if (Vector3.Distance(transform.position, waypointList[currentWP].transform.position) < reach & waypointList[currentWP].GetComponent<WaypointWait>().wait == false)
        {
            NextWP();
        }
        else if (Vector3.Distance(transform.position, waypointList[currentWP].transform.position) < reach & waypointList[currentWP].GetComponent<WaypointWait>().wait == true)
        {
            timer = waypointList[currentWP].GetComponent<WaypointWait>().waitPeriod;
            waiting = true;
        }
    }
    void EnemyPatrol()
    {
        CharacterController controller = GetComponent<CharacterController>(); //this calls the game controller.
        if (controller.isGrounded) //this checks to see if the character is touching the ground.
        {
            //gets enemy to look at target
            Vector3 flatVecToTgt = waypointList[currentWP].transform.position - transform.position;
            flatVecToTgt.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(flatVecToTgt), turnSpeed * Time.deltaTime);

            //makes the enemy move towards current waypoint.
            Vector3 myTrans = waypointList[currentWP].transform.position - transform.position;
            myTrans.y = 0f;
            moveDirection = Vector3.Lerp(moveDirection, myTrans.normalized * enemySpeed, changeOfDirection * Time.deltaTime); //lerps from current waypoint to the next.
        }
        moveDirection.y -= gravity * Time.smoothDeltaTime; //applies gravity when character is off the ground.
        controller.Move(moveDirection * Time.smoothDeltaTime); //applies the movement to the controller.
    }

    void EnemyStop()
    {
        CharacterController controller = GetComponent<CharacterController>(); //this calls the gamecontroller.
        if (controller.isGrounded) //this checks to see if the character is touching the ground.
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero * enemySpeed, changeOfDirection * Time.deltaTime); //lerps the current movement to a stop.
        }
        moveDirection.y -= gravity * Time.smoothDeltaTime; //applies gravity when character is off the ground.
        controller.Move(moveDirection * Time.smoothDeltaTime); //applies the movement to the controller.
    }

    IEnumerator waitEnum(float seconds)
    {
        waiting = false; //turns waiting back off so it doesn't constantly update.
        walk = false; //turns walk off so the enemy stop animation is played.
        yield return new WaitForSeconds(seconds); //wait for three seconds.
        waiting = false;//after waiting, turn waiting back off.
        walk = true; //and then turn walk back on.
        NextWP(); //grab the next waypoint and continue on your merry way.
    }

    void NextWP() //keeps the waypoints rolling.
    {
        currentWP = currentWP + 1;
        if (currentWP >= waypointList.Length)
        {
            currentWP = 0;
        }
    }

    void Start()
    {
        currentWP = 0; //sets the first waypoint in the array.
        player = GameObject.FindGameObjectWithTag("Player");
        GoalObj = GameObject.FindGameObjectWithTag("exit");
    }

    void Update() // a condition that runs enemyStop() when gameovermanager.seen=true and sets his rotation to Quaternion.LookRotation straight at player.
    {
        if (GoalObj.GetComponent<ExitLevel>().goalReached)
        {
            EnemyStop(); //runs the enemy stop animation.
            //deactivate detects?
        }
        else
        {
            if (gameObject.GetComponent<Detection>().seeyou) //checks to see if player has been seen and ignores the rest of the conditions if so.
            {
                EnemyStop();

                Vector3 lookAtPlyr = player.transform.position - transform.position;
                lookAtPlyr.y = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtPlyr), turnSpeed * Time.deltaTime); //last three lines make enemy look at player when player discovered.
            }
            else
            {
                if (waiting) //this checks to see if waiting has been turned on by the WaypointDetect proc.
                {
                    StartCoroutine(waitEnum(timer)); //calls on a coroutine to start the wait.
                }

                if (walk) //if waiting is on. Do the Patrol
                {
                    WaypointDetect(); //runs the waypoint selection code.
                    EnemyPatrol(); //runs the patrol animation.
                }
                else //once waiting is set to off, then walk is set to off as well, so this part runs.
                {
                    EnemyStop(); //runs the enemy stop animation.
                }
            }
        }
        //add waitTurn functionality.
    }
}