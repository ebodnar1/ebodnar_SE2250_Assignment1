using UnityEngine;

//Allows the camera to remain focused on the ball
public class CameraControl : MonoBehaviour
{
    /*
     * public GameObject to represent the sphere and private 
     * 3D vector to represent the offset of the camera from the ball
    */
    public GameObject player;
    private Vector3 offset;

    //Determines initial offset of the camera from the ball
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    /*
     * Continuously updates the position of the camera as the ball moves
     * Uses LateUpdate to update the camera after the ball's position
    */
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
    }
}
