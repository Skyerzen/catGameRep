using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

    private bool detec;
    private bool exit;
    Animator anim;
    public GameObject gameUI;
    public GameObject GoalObj;
    // Use this for initialization
    void Awake ()
    {
        anim = GetComponent<Animator>();
        gameUI = GameObject.FindGameObjectWithTag("UI");
        GoalObj = GameObject.FindGameObjectWithTag("exit");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //applying animations to character.
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 flatVec = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        detec = gameUI.GetComponent<GameOverManager>().seen;
        exit = GoalObj.GetComponent<ExitLevel>().goalReached;
        float vel = flatVec.magnitude * .2f;
        float grav = controller.velocity.y;
            anim.SetFloat("walkFloat", vel);
            anim.SetFloat("speed", vel);
            anim.SetFloat("fall", grav * -1f);
            anim.SetBool("discovered", detec);
            anim.SetBool("lvlClear", exit);
    }
}
