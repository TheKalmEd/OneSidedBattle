using UnityEngine;

public class PistolAnimShot : MonoBehaviour
{
    public Pistol ThePistol;
    public GameObject ThePistolGameObject;
    public void PistolAnimationEventShoot()
    {
        ThePistol.Shoot();
    }

    public void PausePistolAnimation()
    {
        if (ThePistol.isReloading)
        {
            ThePistol.PlayerAnim.SetFloat("ReloadSpeed", 0);
        }
    }

    public void ShowPistol()
    {
        ThePistolGameObject.SetActive(true);
    }
}
