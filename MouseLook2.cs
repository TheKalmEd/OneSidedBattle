using UnityEngine;

public class MouseLook2 : MonoBehaviour
{
    public float mouseSensitivity = 100f;


    public Transform playerBody;

    float xRotation = 0f;
    public float CameraRotationX;
    public GameObject TheCameraGameObject;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        CameraRotationX = TheCameraGameObject.transform.rotation.eulerAngles.x;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30, 45);


        playerBody.localRotation = Quaternion.Euler(CameraRotationX , 5, 0f);

    }
}
