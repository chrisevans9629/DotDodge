using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : PlayerBase
{
    public Rigidbody2D rb;
    protected override void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        rb.AddForce(new Vector2(0, Jump * Time.deltaTime), ForceMode2D.Force);
    }
}