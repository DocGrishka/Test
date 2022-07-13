using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class Shooting : BaseWeapon
{
    // Start is called before the first frame update
    public GameObject prefab;
    public InputActionReference Shoot_Right = null;
    public InputActionReference Shoot_Left = null;
    public int Power;
    public int DamagePower;
    public bool CanPress=true;
    void Start()
    {
        ReturnToDefaultPlace();

    }
    /// <summary>
    /// Выстрел
    /// </summary>
    public void Shoot()
    {

        var bullet = Instantiate(prefab,transform);
        bullet.transform.SetParent(null); ;
        bullet.AddComponent<BulletScript>().type = type;
        bullet.GetComponent<BulletScript>().damage = DamagePower;
        bullet.AddComponent<Rigidbody>().AddForce(transform.forward* Power
            , ForceMode.Impulse);
    
    }
    // Update is called once per frame
    void Update()
    {
        if (Hand)//если мы взяли объект в руку
        {//определяем контроллер
            if (Hand.ToString().Contains("Right"))
            {
                if (Shoot_Right.action.ReadValue<float>() == 1 && InHand && CanPress)
                {
                    Shoot();
                    CanPress = false;
                }
                if (Shoot_Right.action.ReadValue<float>() == 0)
                    CanPress = true;
            }
            if (Hand.ToString().Contains("Left"))
            {
                if (Shoot_Left.action.ReadValue<float>() == 1 && InHand && CanPress)
                {
                    Shoot();
                    CanPress = false;
                }
                if (Shoot_Left.action.ReadValue<float>() == 0)
                    CanPress = true;
            }
           
        }
        else
            CanPress = true;

    }
}
