using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : PlayerBase
{
    public Rigidbody2D rb;
    public AudioSource jumpSound;
    protected override void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, Jump * Time.deltaTime), ForceMode2D.Impulse);
        jumpSound.PlayOneShot(jumpSound.clip);
        Debug.Log($"Velocity: {rb.velocity}");
    }
    
   

    public void StartGame()
    {
        rb.simulated = true;
        GameIsRunning = true;
    }
}