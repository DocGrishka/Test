using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class BaseWeapon : MonoBehaviour
{
    public bool InHand = false;
    public int type = 0;
    public GameObject SpawnPlace;
    public Transform Hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReturnToDefaultPlace()
    {
        transform.parent.transform.position = SpawnPlace.transform.position;
        transform.parent.transform.rotation= SpawnPlace.transform.rotation;
    }
    /// <summary>
    /// При взятии асоциируем руку
    /// </summary>
    /// <param name="a"></param>
    public void Grab(SelectEnterEventArgs a)
    {
        Hand = a.interactorObject.transform;
            InHand = true;
        
    }
    /// <summary>
    /// При выбрасывании возращаем объект на место
    /// </summary>
    /// <param name="a"></param>
    public void OutGrab(SelectExitEventArgs a)
    {
        InHand = false;
        Hand = null; ;
        ReturnToDefaultPlace();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
