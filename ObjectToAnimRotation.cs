using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectToAnimRotation : MonoBehaviour
{
   
    public Transform ObjectBody;

    public float AnimRotationX;
    public float AnimRotationY;
    public float AnimRotationZ;
    public GameObject TheAnimGameObject;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
        AnimRotationX = TheAnimGameObject.transform.rotation.eulerAngles.x;
        AnimRotationY = TheAnimGameObject.transform.rotation.eulerAngles.y;
        AnimRotationZ = TheAnimGameObject.transform.rotation.eulerAngles.z + 180;


        ObjectBody.localRotation = Quaternion.Euler(AnimRotationX, AnimRotationY, AnimRotationZ);

    }



}
