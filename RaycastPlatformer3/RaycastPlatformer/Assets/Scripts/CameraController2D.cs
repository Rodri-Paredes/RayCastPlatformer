using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform player;      
    public float offset = 0.5f;    
    public float leftLimit = 0f;   

    void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);

        if (newPosition.x < leftLimit)
        {
            newPosition.x = leftLimit;
        }

        transform.position = newPosition;
    }
}
