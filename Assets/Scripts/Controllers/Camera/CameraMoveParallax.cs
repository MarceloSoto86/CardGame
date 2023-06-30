using UnityEngine;

public class CameraMoveParallax : MonoBehaviour
{
    [SerializeField]
    private float cameraMoveSpeed = 0.25f;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;

    }

    private void Update()
    {
        Vector3 newPosition = cameraTransform.position;
        newPosition.x += cameraMoveSpeed * Time.deltaTime;
        cameraTransform.position = newPosition;
    }

}
