using UnityEngine;
using System.Collections;

public class EnemyAnimations : MonoBehaviour {

    private bool det; 
    Animator anim;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //applying animations to character.
        CharacterController controller = GetComponent<CharacterController>();
        det = GetComponent<Detection>().seeyou;
        Vector3 flatVec = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float vel = flatVec.magnitude * .2f;
        float grav = controller.velocity.y;
            anim.SetFloat("walkFloat", vel);
            anim.SetFloat("speed", vel);
            anim.SetFloat("fall", grav * -1f);
            anim.SetBool("found", det);
    }
}
