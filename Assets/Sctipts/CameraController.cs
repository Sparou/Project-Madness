using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector2 minSceneCoordinates;
    [SerializeField] Vector2 maxSceneCoordinates;
    [SerializeField] float cameraSpeed = 2f; 

    void Update()
    {
        Vector3 targetPostion = playerTransform.position;
        Vector3 boundPosition = new Vector3 (
            Mathf.Clamp(targetPostion.x, minSceneCoordinates.x, maxSceneCoordinates.x),
            Mathf.Clamp(targetPostion.y, minSceneCoordinates.y, maxSceneCoordinates.y), -10);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, cameraSpeed * Time.deltaTime);

        transform.position = smoothPosition;
        
    }
}
