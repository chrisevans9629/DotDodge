using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : PlayerBase
{
    
    //public float Speed;
    public Rigidbody rb;

    protected override void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        rb.AddForce(0, Jump * Time.deltaTime, 0, ForceMode.Impulse);
    }

   
}
