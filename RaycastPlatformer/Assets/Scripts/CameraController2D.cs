using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform player;
    public float offset = 0.5f;
    public float leftLimit = 0f;   

    private float minXPosition;    

    void Start()
    {
        minXPosition = transform.position.x;
    }

    void Update()
    {
        if (player.position.x > transform.position.x + offset)
        {
            Vector3 newPosition = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);
            transform.position = newPosition;

            minXPosition = transform.position.x;
        }

        if (player.position.x < minXPosition - offset)
        {
            player.position = new Vector3(minXPosition - offset, player.position.y, player.position.z);
        }

        if (player.position.x < leftLimit)
        {
            player.position = new Vector3(leftLimit, player.position.y, player.position.z);
        }
    }
}
