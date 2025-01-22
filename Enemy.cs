using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Enemy : MonoBehaviour
{
    public float Health = 100;
    public bool HasDied = false;
    public Animator TheEnemyAnimator;
    public float HitAnimationTimer = 0;
    private bool IsKneeling = false;
    public bool LastShotWasLeg = false;
    public bool HasExploded = false;
    private void Awake()
    {
        HasDied = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        DisableHitReaction();

        if (HasDied) 
        {
            return;
        }

        if (Health <= 0 && !HasDied)
        {
            Die();
        }

        if (Health <= 5 && !HasDied && LastShotWasLeg)
        {
            TheEnemyAnimator.SetBool("IsKneel", true);
            IsKneeling = true;
        }

       
    }


    private void Die()
    {
       
        if (!IsKneeling)
        {
            
                int _DieNumber = Random.Range(0, 5);
                if (_DieNumber == 0)
                {
                    TheEnemyAnimator.SetBool("IsDie1", true);
                    HitAnimationTimer = 0.1f;
                }
                if (_DieNumber == 1)
                {
                    TheEnemyAnimator.SetBool("IsDie2", true);
                    HitAnimationTimer = 0.1f;
                }
                if (_DieNumber == 2)
                {
                    TheEnemyAnimator.SetBool("IsDie3", true);
                    HitAnimationTimer = 0.1f;
                }
                if (_DieNumber == 3)
                {
                    TheEnemyAnimator.SetBool("IsDie4", true);
                    HitAnimationTimer = 0.1f;
                }
                if (_DieNumber == 4)
                {
                    TheEnemyAnimator.SetBool("IsDie5", true);
                    HitAnimationTimer = 0.1f;
                }
           

        }
        else
        {
            TheEnemyAnimator.SetBool("IsKneel", false);
            TheEnemyAnimator.SetBool("IsDieKneel", true);
        }

        HasDied = true;
    }

    public void HitReact()
    {
        if (HasDied)
        { 
            return;
        }


        if (!IsKneeling)
        {
            int _HitNumber = Random.Range(0, 3);
            if (_HitNumber == 0)
            {
                TheEnemyAnimator.SetBool("IsHit1", true);
                HitAnimationTimer = 0.1f;
            }
            if (_HitNumber == 1)
            {
                TheEnemyAnimator.SetBool("IsHit2", true);
                HitAnimationTimer = 0.1f;
            }
            if (_HitNumber == 2)
            {
                TheEnemyAnimator.SetBool("IsHit3", true);
                HitAnimationTimer = 0.1f;
            }
        }
    }

    private void LateUpdate()
    {
      //  TheEnemyAnimator.SetBool("IsHit1", false);
      //  TheEnemyAnimator.SetBool("IsHit2", false);
      //  TheEnemyAnimator.SetBool("IsHit3", false);
    }

    private void DisableHitReaction()
    {
        HitAnimationTimer -= Time.deltaTime;

        if (HitAnimationTimer <= 0)
        {
            TheEnemyAnimator.SetBool("IsHit1", false);
            TheEnemyAnimator.SetBool("IsHit2", false);
            TheEnemyAnimator.SetBool("IsHit3", false);
        }
    }

    public IEnumerator HasExplodedTimer()
    {
            yield return new WaitForSeconds(2);
            HasExploded = false;
    }

}
