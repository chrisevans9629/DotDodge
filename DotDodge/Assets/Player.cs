using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
        Time.timeScale = 1;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, Jump * Time.deltaTime, 0, ForceMode.Impulse);
        }

        if (Time.timeScale > 0)
        {
            Score++;
            text.text = "Score: " + Score;
        }
    }
}
