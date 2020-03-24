using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : PlayerBase
{
    public Rigidbody2D rb;
    public AudioSource jumpSound;
    protected override void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Jump * Time.fixedDeltaTime), ForceMode2D.Impulse);
            jumpSound.PlayOneShot(jumpSound.clip);
        }
        else
        {
            
        }
    }
    
   

    public void StartGame()
    {
        rb.simulated = true;
        GameIsRunning = true;
    }
}