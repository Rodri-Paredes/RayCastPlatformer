using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform player; 
    public float offset = 0.5f;  
    public float leftLimit = 0f;

    void Update()
    {
        // Calcular la nueva posici�n de la c�mara
        Vector3 newPosition = new Vector3(player.position.x - offset, transform.position.y, transform.position.z);

        // No permitir que la c�mara retroceda m�s all� del l�mite izquierdo (0)
        if (newPosition.x < leftLimit)
        {
            newPosition.x = leftLimit;
        }

        // Actualizar la posici�n de la camara
        transform.position = newPosition;
    }
}
