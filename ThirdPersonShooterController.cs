using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using Unity.VisualScripting;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera AimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    public ThirdPersonController TheThirdPersonController;
    public GameObject TargetAimConstrain;
    public MultiAimConstraint TheHeadMultiAimConstrain;
    private float TargetWeight = 0;
    public GameObject PistolGameObject;
    public Pistol ThePistol;
    public bool IsGunMode = false;
    public float GunModeTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GunModeTimer > 0)
        {
            IsGunMode = true;
        }
        else 
        {
            IsGunMode = false;
        }

        TheHeadMultiAimConstrain.weight = Mathf.Lerp(TheHeadMultiAimConstrain.weight, TargetWeight, Time.deltaTime * 10f);

        Vector3 MouseWorldPosition = Vector3.zero;

        Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            MouseWorldPosition = raycastHit.point;
        }

        if (Input.GetButton("Fire2"))
        {
            Debug.Log("click");
            AimVirtualCamera.gameObject.SetActive(true);
            TheThirdPersonController.IsAiming = true;

            Vector3 worldAimTarget = MouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
           // PistolGameObject.transform.forward = Vector3.Lerp(TheThirdPersonController.transform.forward, aimDirection, Time.deltaTime * 20f);
            TargetAimConstrain.transform.position = MouseWorldPosition;
            TheThirdPersonController.Sensitivity = TheThirdPersonController.AimSensitivity;
            TargetWeight = 1;
        }
        else
        {
            AimVirtualCamera.gameObject.SetActive(false);
            if (GunModeTimer <= 0)
            {
                TheThirdPersonController.IsAiming = false;
            }
            
            TargetWeight = 0;

            TheThirdPersonController.Sensitivity = TheThirdPersonController.NormalSensitivity;
        }

        if (TheThirdPersonController.IsAiming) 
        {
            Vector3 worldAimTarget = MouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
    }


}
