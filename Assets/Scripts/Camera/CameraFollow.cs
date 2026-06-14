using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 15f;
    [SerializeField] private float height = 5f;
    [SerializeField] private float smoothSpeed = 5f;
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + target.forward * 5f);
    }
}
