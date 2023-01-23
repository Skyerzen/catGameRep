using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    //Game managing
    public GameObject gameUI;
    public GameObject GoalObj;
    public GameObject[] Enemies;
    
    //Screen Joystick.
    private Vector2 scrnCntr; //This will contain the center of screen.
	private Vector2 scrnJoystick; //This will contain the mouse screen position.
    private float deadStick = .4f;
	
	//Character Controller.
	public float moveSpeed = 8F; //Controls the speed our character runs.
    public float gravity = 20.0F; //Gravity multiplier.
    private Vector3 moveDirection = Vector3.zero; //Character's vector of movement.

    //Character Smoothers.
    //desceleration
    public float rateOfDeceleration = 6f;
	public float rateOfAcceleration = 6f;
		//rotation
	public float turnSpeed = 12f;
    public float reverse; //this turns the controller into reverse controller
    public Toggle movToggle;

    //Game Functions
    //**********************************************


    void Awake ()
    {
        gameUI = GameObject.FindGameObjectWithTag("UI");
        GoalObj = GameObject.FindGameObjectWithTag("exit");
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        movToggle = GameObject.Find("ReverseToggle").GetComponent<Toggle>(); //fix this line of code!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        scrnCntr.Set (Screen.width *.5f, Screen.height * .5f);        //this halves the resolution to find the center

        if (PlayerPrefs.GetInt("Rev") == 1) //this sets the reverse option to whatever it was set last.
        {
            movToggle.isOn = true;
            reverse = -1;
        }
        else if (PlayerPrefs.GetInt("Rev") == 0)
        {
            movToggle.isOn = false;
            reverse = 1;
        }

    }

    public void ReverseOnClick()
    {
        if (movToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Rev", 0);
            PlayerPrefs.Save();
            reverse = 1;
        }
        else if (movToggle.isOn)
        {
            PlayerPrefs.SetInt("Rev", 1);
            PlayerPrefs.Save();
            reverse = -1;
        }
    }

        void Update ()//adds a condition so when detection.seeyou is true, the character movement is set to zero and the caught animation is played.
    {
        if (GoalObj.GetComponent<ExitLevel>().goalReached)
        {
            PlayerStops();

        }
        else {
            if (gameUI.GetComponent<GameOverManager>().seen)
            {
                FoundByEnemy();
                PlayerStops();
            }
            else
            {
                ScreenJoystick();
                PlayerMove();
            }
        }
    }

    //***********************************************
    //my functions

    void FoundByEnemy()
    {
        CharacterController controller = GetComponent<CharacterController>();
        foreach (GameObject currentEnemy in Enemies)
        {
            if (currentEnemy.GetComponent<Detection>().seeyou) //Just to avoid confusion in the future, this script goes through all the Detection scripts running in the scene...
            {
                Vector3 lookAtEnemy = currentEnemy.transform.position - transform.position;
                lookAtEnemy.y = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtEnemy), turnSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, currentEnemy.transform.position) < 1.5f)
            {
                Vector3 enemyPlayerVec = (transform.position - currentEnemy.transform.position)*2f;
                moveDirection = Vector3.Lerp(moveDirection, new Vector3(enemyPlayerVec.x, 0, enemyPlayerVec.z), rateOfAcceleration * Time.deltaTime);
                moveDirection.y = 0f;
                controller.Move(moveDirection * Time.smoothDeltaTime); //applies the motion vector onto the character.
            }
        }
    }


    void PlayerMove()
    {
        CharacterController controller = GetComponent<CharacterController>(); //this sets a variable to call the character controller much easier I think.
        if (controller.isGrounded) //this checks to see if the character is touching the ground.
        {
            if (Input.GetKey(KeyCode.Mouse0)) //this makes sure I'm pressing the mouse button before doing anything.
            {
                moveDirection = Vector3.Lerp(moveDirection, new Vector3(scrnJoystick.x, 0, scrnJoystick.y), rateOfAcceleration * Time.deltaTime); //for some reason, it feeds a -y number which throws everything off...
                moveDirection.y = 0f;

                if (Vector2.SqrMagnitude(scrnJoystick) > deadStick)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), turnSpeed * Time.deltaTime);//this handles the heading of the character so he always faces the direction he runs.
                }
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, rateOfDeceleration * Time.deltaTime); //dampens the deceleration.
            }
        }
        moveDirection.y -= gravity * Time.smoothDeltaTime; //applies gravity.
        controller.Move(moveDirection * Time.smoothDeltaTime); //applies the motion vector onto the character so character keeps falling forward.
    }

    void ScreenJoystick()
    {
        //this gets the screen position of the mouse on the screen
        scrnJoystick = Input.mousePosition;
        //this centers it and Normalizes the value(0-10). ????????????????
        scrnJoystick = (scrnJoystick - scrnCntr);
        scrnJoystick = scrnJoystick * reverse;
        scrnJoystick.Set(scrnJoystick.x / scrnCntr.x * moveSpeed, scrnJoystick.y / scrnCntr.y * moveSpeed);

        scrnJoystick = Vector2.ClampMagnitude(scrnJoystick, moveSpeed);
        if (Vector2.SqrMagnitude(scrnJoystick) < deadStick)
        {
            scrnJoystick = Vector2.zero;
        }

    }
    void PlayerStops()
    {
        CharacterController controller = GetComponent<CharacterController>(); //this sets a variable to call the character controller much easier I think.
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, rateOfDeceleration * Time.deltaTime); //dampens the deceleration.
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }
}
