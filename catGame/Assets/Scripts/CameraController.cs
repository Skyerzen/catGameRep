using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float hControl =20f;
    public float aControl =5f;
    public float cameraDelay = .1f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //finds the player
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + hControl, Player.transform.position.z - aControl); //positions camera above player at the beginning of scene.
        Vector3 cameraRotation = (Player.transform.position - transform.position);
        cameraRotation.x = 0f;
        transform.rotation = Quaternion.LookRotation(cameraRotation); //last three lines try to rotate camera to initial position.
    }

    void Update ()
    {
        float height = Player.transform.position.y + hControl;
        float azimuth = Player.transform.position.z - aControl; //These two control the offset of the camera to the player.

        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, height, azimuth), Time.deltaTime * cameraDelay);
        Vector3 cameraRotation = (Player.transform.position - transform.position);
        cameraRotation.x = 0f;
        transform.rotation = Quaternion.LookRotation(cameraRotation);
    }
}
