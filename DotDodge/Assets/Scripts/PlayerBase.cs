using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class PlayerBase : MonoBehaviour
{
    public string[] IgnoreTags;
    public float Score;
    public Text text;
    public float Jump;
    public GameObject FirePosition;
    public float FireRateSeconds;
    public UnityEvent PlayerDied;
    public GameObject Bullet;
    public bool GameIsRunning = true;
    private PlayerInputActions input;
    // Start is called before the first frame update
    public void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Jump.performed += JumpOnPerformed;
    }

    protected abstract void JumpOnPerformed(InputAction.CallbackContext obj);
    public void Start()
    {
        StartCoroutine(Fire());
        Time.timeScale = 1;

    }

    public void OnDisable()
    {
        input.Disable();
    }

    public void OnEnable()
    {
        input.Enable();
    }


    public void OnDestroy()
    {
        input.Player.Jump.performed -= JumpOnPerformed;

        input.Dispose();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IgnoreTags != null && IgnoreTags.Contains(other.tag))
            return;
        if (!other.CompareTag("Bullet"))
        {
            UpdateHighscore();
            PlayerDied.Invoke();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void UpdateHighscore()
    {
        var scoreKey = "score";
        if (PlayerPrefs.HasKey(scoreKey))
        {
            var score = PlayerPrefs.GetFloat(scoreKey);
            if (Score > score)
            {
                PlayerPrefs.SetFloat(scoreKey, Score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(scoreKey, Score);
        }
    }

    public void OnTriggerEnter(Collider other)
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
            if (GameIsRunning)
                Instantiate(Bullet, FirePosition.transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    public void Update()
    {
        //if (input.Player.Jump.ReadValue<float>() > 0)
        //{
        //}

        if (Time.timeScale > 0 && GameIsRunning)
        {
            Score++;
            text.text = "Score: " + Score;
        }
    }
}