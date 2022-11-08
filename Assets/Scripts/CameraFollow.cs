using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;

    [Header("Lerp Parameters")] 
    [SerializeField] [Range(0, 1)] private float positionLerp;
    [SerializeField] [Range(0, 1)] private float rotationLerp;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, positionLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraTarget.rotation, rotationLerp);
    }
}