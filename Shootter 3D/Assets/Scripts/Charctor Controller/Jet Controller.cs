using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetController : MonoBehaviour
{
    public bool isDashing;
    private int dashattempts;
    private float dashStartTime;

    PlayerController playerController;
    CharacterController cc;

    [SerializeField] ParticleSystem FRONT;
    [SerializeField] ParticleSystem BACK;
    [SerializeField] ParticleSystem LEFT;
    [SerializeField] ParticleSystem RIGHT;
    public void Update()
    {
        HandleDash();
        playerController= GetComponent<PlayerController>(); 
        cc=GetComponent<CharacterController>();
    }
    void HandleDash()
    {
        bool istryingtoDash = Input.GetKeyDown(KeyCode.E);
        if (istryingtoDash&&!isDashing)
        {
            if (dashattempts <=50) {
                StartDash();
            
            }
        
        
        }
        if (isDashing)
        {
            if (Time.time - dashStartTime <= 0.4f) {
                if (playerController.move.Equals(Vector3.zero))
                {
                    cc.Move(transform.forward * 30f * Time.deltaTime);
                }
                else
                {
                    cc.Move(playerController.move.normalized * 30 * Time.deltaTime);
                }
            }
            else
            {
                EndDash();
            }

        }
    }
    void StartDash()
    {
        isDashing=true; ;
       dashStartTime=Time.time;
        dashattempts -= 1;
        PlaydashParticle();
    }

    void EndDash()
    {
        isDashing = false;
        dashStartTime = 0;

    }
    void PlaydashParticle()
    {
        Vector3 inputvector=playerController.inputVector;
        if (inputvector.z > 0 && Mathf.Abs(inputvector.x) <= inputvector.z)
        {
            FRONT.Play();
            return;
        }
        if (inputvector.z < 0 && Mathf.Abs(inputvector.x) <=Mathf.Abs( inputvector.z))
        {
            BACK.Play();
            return;
        }
        if (inputvector.x > 0 )
        {
            RIGHT.Play();
            return;
        }
        if(inputvector.x< 0)
        {
            LEFT.Play();
            return;
        }
        FRONT.Play();
    }
}
