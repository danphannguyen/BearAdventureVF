using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target; // Le joueur
    public float verticalSmoothTime = 0.2f;
    public float horizontalSmoothTime = 0.05f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // On suit la cible, mais on lisse la hauteur (Y)
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 horizontalPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, horizontalSmoothTime);

        // Séparé pour Y
        float smoothedY = Mathf.Lerp(transform.position.y, target.position.y + 2f, verticalSmoothTime);
        transform.position = new Vector3(horizontalPos.x, smoothedY, horizontalPos.z);
    }
}
