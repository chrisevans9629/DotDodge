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
        if (obj.performed)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(0, Jump * Time.fixedDeltaTime, 0, ForceMode.Impulse);
        }
    }

    public void StartGame()
    {
        rb.isKinematic = false;
        GameIsRunning = true;
    }
}
