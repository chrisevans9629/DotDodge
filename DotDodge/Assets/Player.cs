using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Score;
    public Text text;
    public float Jump;
    //public float Speed;
    public Rigidbody rb;
    public GameObject FirePosition;
    public float FireRateSeconds;
    public UnityEvent PlayerDied;
    public GameObject Bullet;

    private PlayerInputActions input;
    // Start is called before the first frame update
    void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Jump.performed += JumpOnPerformed;
    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        rb.AddForce(0, Jump * Time.deltaTime, 0, ForceMode.Impulse);
    }

    void Start()
    {
        StartCoroutine(Fire());
        Time.timeScale = 1;

    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnEnable()
    {
        input.Enable();
    }


    void OnDestroy()
    {
        input.Player.Jump.performed -= JumpOnPerformed;

        input.Dispose();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
        {
            PlayerDied.Invoke();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(FireRateSeconds);
            Instantiate(Bullet, FirePosition.transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (input.Player.Jump.ReadValue<float>() > 0)
        //{
        //}

        if (Time.timeScale > 0)
        {
            Score++;
            text.text = "Score: " + Score;
        }
    }
}
