using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class particleBull : BaseWeapon
{
    public int damage=0;
   
    
    public InputActionReference Shoot_Right = null;
    public InputActionReference Shoot_Left = null;
    // Start is called before the first frame update
    void Start()
    {
        ReturnToDefaultPlace();
    }


    public GameObject Pug;
    // Update is called once per frame
    void Update()
    {
        if (Hand)
        {
            if (Hand.ToString().Contains("Right"))
            {
                
                if (Shoot_Right.action.ReadValue<float>() == 1 && InHand)
                {
                    GetComponent<ParticleSystem>().emissionRate = 10;
                }
               if (Shoot_Right.action.ReadValue<float>() == 0)
                    GetComponent<ParticleSystem>().emissionRate = 0;
            }
            if (Hand.ToString().Contains("Left"))
            {
               
                if (Shoot_Left.action.ReadValue<float>() == 1 && InHand)
                {
                    GetComponent<ParticleSystem>().emissionRate = 10;
                }
                if (Shoot_Left.action.ReadValue<float>() == 0)
                    GetComponent<ParticleSystem>().emissionRate = 0;
            }

        }
        else
            GetComponent<ParticleSystem>().emissionRate = 0;


            


    }
    void OnParticleTrigger()
    {
        //находим все частицы которые вошли в наш объект(объекты)
        List<ParticleSystem.Particle> enteredParticles = new List<ParticleSystem.Particle>();
        int enterCount = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteredParticles);
        
        foreach (ParticleSystem.Particle particle in enteredParticles)
        {
            Pug.GetComponent<Pugalo>().GetDamage(damage, type);
        }
    }
}
