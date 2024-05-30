using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Camera mainCamera;
    private float xMin, xMax, yMin, yMax;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        UpdateCameraBoundaries();
    }

    void UpdateCameraBoundaries()
    {
        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        xMin = cameraBottomLeft.x;
        xMax = cameraTopRight.x;
        yMin = cameraBottomLeft.y;
        yMax = cameraTopRight.y;
    }

    public Vector3 GetClampedPosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, xMin, xMax),
            Mathf.Clamp(position.y, yMin, yMax),
            position.z);
    }
}
