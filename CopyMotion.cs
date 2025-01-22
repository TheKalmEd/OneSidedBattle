using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using ActiveRagdoll;
public class CopyMotion : MonoBehaviour
{
    public Transform TargetLimb;
    ConfigurableJoint CJ;
    private ConfigurableJoint joint;

    public float TargetRotationX;
    public float TargetRotationY;
    public float TargetRotationZ;

    public Transform ThisJointTransform;
    public Quaternion JointStart;


    void Start()
    {
        CJ = GetComponent<ConfigurableJoint>();
        joint = GetComponent<ConfigurableJoint>();
        ThisJointTransform = GetComponent<Transform>();
        JointStart = ThisJointTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //CJ.targetRotation = Quaternion.Inverse(TargetLimb.rotation);
        //CJ.targetRotation = TargetLimb.localRotation;
        // SetTargetRotationInternal();

       // TargetRotationX = TargetLimb.transform.rotation.eulerAngles.x;
       // TargetRotationY = TargetLimb.transform.rotation.eulerAngles.y;
       // TargetRotationZ = TargetLimb.transform.rotation.eulerAngles.z + 180;

        CJ.SetTargetRotationLocal(TargetLimb.localRotation, JointStart);

        //CJ.SetTargetRotation(TargetLimb.rotation, ThisJointTransform.rotation);
    }

    
}
