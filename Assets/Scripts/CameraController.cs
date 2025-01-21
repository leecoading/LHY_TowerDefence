using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    [SerializeField] private GameObject CameraMoveArea;
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;

    private Vector3 areaMin; // CameraMoveArea's minimum bounds
    private Vector3 areaMax; // CameraMoveArea's maximum bounds

    private void Start()
    {
        // Get the bounds of CameraMoveArea in world space
        if (CameraMoveArea.TryGetComponent<BoxCollider>(out var collider))
        {
            Bounds bounds = collider.bounds; // Use world-space bounds
            areaMin = bounds.min;
            areaMax = bounds.max;
        }
        else
        {
            Debug.LogError("CameraMoveArea must have a BoxCollider!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            movement += Vector3.forward * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            movement += Vector3.back * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            movement += Vector3.right * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            movement += Vector3.left * panSpeed * Time.deltaTime;
        }

        // Apply movement
        transform.Translate(movement, Space.World);

        // Adjust camera zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Clamp position to CameraMoveArea bounds
        pos.x = Mathf.Clamp(pos.x, areaMin.x, areaMax.x);
        pos.z = Mathf.Clamp(pos.z, areaMin.z, areaMax.z);

        transform.position = pos;
    }
}

