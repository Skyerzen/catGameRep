using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject GoalObj;
    public GameObject[] Enemies;

    private CharacterController controller;

    public float moveSpeed = 8F;
    public float gravity = 20.0F;
    public float rateOfDeceleration = 6f;
    public float rateOfAcceleration = 6f;
    public float turnSpeed = 12f;
    public float reverse = 1; // Default to not reverse
    public float sensitivity = 1;
    public Toggle movToggle;
    public Toggle scrnToggle;
    public bool screenCenter;

    private Vector2 currentTouchPosition;
    private Vector2 initialTouchPosition;
    private Vector2 scrnCntr;
    private Vector2 center;
    private Vector2 scrnJoystick;
    private float deadStick = 0.4f;

    void Awake()
    {
        InitializeReferences();
        LoadPlayerPrefs();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("esc pressed");
        }*/

        if (GoalObj.GetComponent<ExitLevel>().goalReached)
        {
            PlayerStops();
        }
        else
        {
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
        
        if (gameUI.GetComponent<RevTgl>().reversa == true)
        {
            reverse = -1;
        }else
        {
            reverse = 1;
        }

        if (gameUI.GetComponent<ScrnTgl>().pantalla == true)
        {
            screenCenter = false;
            sensitivity = 3.5f;
        }
        else
        {
            screenCenter = true;
            sensitivity = 1f;
        }
    }

    void InitializeReferences()
    {
        controller = GetComponent<CharacterController>();
        gameUI = GameObject.FindGameObjectWithTag("UI");
        GoalObj = GameObject.FindGameObjectWithTag("exit");
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        movToggle = GameObject.Find("ReverseToggle").GetComponent<Toggle>();
        scrnToggle = GameObject.Find("ScreenToggle").GetComponent<Toggle>();
        scrnCntr = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void LoadPlayerPrefs()
    {
        int revValue = PlayerPrefs.GetInt("Rev", 0);
        movToggle.isOn = revValue == 1;
        reverse = movToggle.isOn ? -1 : 1;

        int scrnValue = PlayerPrefs.GetInt("Scrn", 0);
        scrnToggle.isOn = scrnValue == 1;
        screenCenter = scrnToggle.isOn;
    }

    void FoundByEnemy()
    {
        foreach (GameObject currentEnemy in Enemies)
        {
            if (currentEnemy.GetComponent<Detection>().seeyou)
            {
                Vector3 lookAtEnemy = currentEnemy.transform.position - transform.position;
                lookAtEnemy.y = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtEnemy), turnSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, currentEnemy.transform.position) < 1.5f)
            {
                Vector3 enemyPlayerVec = (transform.position - currentEnemy.transform.position) * 2f;
                moveDirection = Vector3.Lerp(moveDirection, new Vector3(enemyPlayerVec.x, 0, enemyPlayerVec.z), rateOfAcceleration * Time.deltaTime);
                moveDirection.y = 0f;
                controller.Move(moveDirection * Time.smoothDeltaTime);
            }
        }
    }

    private Vector3 moveDirection = Vector3.zero;

    void PlayerMove()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButton(0))
            {
                moveDirection = Vector3.Lerp(moveDirection, new Vector3(scrnJoystick.x, 0, scrnJoystick.y), rateOfAcceleration * Time.deltaTime);
                moveDirection.y = 0f;

                if (scrnJoystick.sqrMagnitude > deadStick * deadStick)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), turnSpeed * Time.deltaTime);
                }
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, rateOfDeceleration * Time.deltaTime);
            }
        }
        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }
    /*
    I need to write a movable joystick center:
    if the location of the current touch (A) is greater than the previous touch position (B) by more than a certain number, then make the center that location
    */
    void ScreenJoystick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (screenCenter)
            {
                center = scrnCntr;
            }
            else
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initialTouchPosition = touch.position;
                }
                center = initialTouchPosition;
            }

            currentTouchPosition = touch.position;
        }

        scrnJoystick = currentTouchPosition;
        scrnJoystick = (scrnJoystick - center);
        scrnJoystick = scrnJoystick * sensitivity * reverse;
        scrnJoystick.Set(scrnJoystick.x / center.x * moveSpeed, scrnJoystick.y / center.y * moveSpeed);
        scrnJoystick = Vector2.ClampMagnitude(scrnJoystick, moveSpeed);
        if (scrnJoystick.sqrMagnitude < deadStick * deadStick)
        {
            scrnJoystick = Vector2.zero;
        }
    }

    void PlayerStops()
    {
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, rateOfDeceleration * Time.deltaTime);
        controller.Move(moveDirection * Time.smoothDeltaTime);
    }
}