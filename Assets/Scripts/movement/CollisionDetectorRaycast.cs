using UnityEngine;

public class CollisionDetectorRaycast : MonoBehaviour
{
    [Header("Raycast Settings")]
    public LayerMask detectionLayers;
    public float rayLength = 1f;
    public Vector3 localDirection = Vector3.forward; // dirección local del rayo
    public bool showRay = true;

    public bool IsColliding { get; private set; }
    public RaycastHit outHit;

    void Update()
    {
        // Convierte la dirección local (por ejemplo, -Vector3.up) a dirección global
        Vector3 worldDirection = transform.TransformDirection(localDirection.normalized);

        bool hitDetected = Physics.Raycast(transform.position, worldDirection, out outHit, rayLength, detectionLayers);
        IsColliding = hitDetected;

        if (showRay)
        {
            Debug.DrawRay(transform.position, worldDirection * rayLength, hitDetected ? Color.green : Color.red);
        }
    }
}
