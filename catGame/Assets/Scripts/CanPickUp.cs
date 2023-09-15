using UnityEngine;
using System.Collections;

public class CanPickUp : MonoBehaviour {

    private bool gate = false;
    private AudioSource pickUpSound;
    Animator anim;
    public GameObject Pow;
    public float wait = 1f;

    //enumerator with timer
    IEnumerator waitDestroy(float seconds)
    {
        gate = true;
        yield return new WaitForSeconds(seconds); //wait for two seconds.
        gameObject.SetActive(false);
        Pow.gameObject.SetActive(true);
    }

    void Awake()
    {
        pickUpSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        Pow = GameObject.FindGameObjectWithTag("powIcon");
        Pow.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider playr) {
        if (playr.gameObject.tag == "Player")
        {
            pickUpSound.Stop();
            pickUpSound.Play();
            anim.SetBool("Spin", true);
            if (gate == false)
            {
                StartCoroutine(waitDestroy(wait));
            }
        }
	}
}
