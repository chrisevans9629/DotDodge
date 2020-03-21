using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : PlayerBase
{
    public Rigidbody2D rb;
    public float MaxVelocity;
    protected override void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        //rb.velocity = new Vector2(0, Jump * Time.deltaTime);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, Jump * Time.deltaTime), ForceMode2D.Impulse);

        //rb.velocity = new Vector2(0, Mathf.Min(MaxVelocity, rb.velocity.y));  
        //rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxVelocity);
    }

    public void StartGame()
    {
        rb.simulated = true;
        GameIsRunning = true;
    }
}