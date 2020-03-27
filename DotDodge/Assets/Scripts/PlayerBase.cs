using System;
using System.Collections;
using System.Linq;
using Assets.Scripts;
using Boo.Lang;
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
    private PlayerInputActions2 input;
    [HideInInspector]
    public bool IsDead;
    public AudioSource GunSound;
    public int BulletCount = 1;
    public SpriteRenderer sprite;
    public Healthbar Healthbar;
    private int _shieldCount;
    //public int ShieldCount
    //{
    //    get => _shieldCount;
    //    set { _shieldCount = value;
    //        OnShieldCountChanged();
    //    }
    //}
    public void AddHealth()
    {
        _shieldCount++;
        if (Healthbar != null)
        {
            Healthbar.AddHealth();
        }
    }

    public void ResetHealth()
    {
        _shieldCount = 0;
        if (Healthbar != null)
        {
            Healthbar.ResetHealth();
        }
    }
    public void RemoveHealth()
    {
        _shieldCount--;
        if (Healthbar != null)
        {
            Healthbar.RemoveHealth();
        }
    }

  

    // Start is called before the first frame update
    public void Awake()
    {
        input = new PlayerInputActions2();
        input.Player.Jump.performed += JumpOnPerformed;
    }

    protected abstract void JumpOnPerformed(InputAction.CallbackContext obj);
    public void Start()
    {
        StartCoroutine(Fire());
        Time.timeScale = 1;
       // sprite = GetComponent<SpriteRenderer>();
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
        ApplyCollision(other.gameObject);
    }

    private void ApplyCollision(GameObject other)
    {
        if (IgnoreTags != null && IgnoreTags.Contains(other.tag))
            return;
        if (!other.CompareTag("Bullet") && !other.CompareTag("Powerup") && !IsDead)
        {
            if (this._shieldCount <= 0)
            {
                IsDead = true;
                UpdateHighscore();
                if (sprite != null)
                {
                    LeanTween.value(this.gameObject, color => sprite.color = color, sprite.color, Color.green, 1f)
                        .setOnComplete(() => PlayerDied.Invoke());
                }
            }
            else
            {
                RemoveHealth();
            }
            // StartCoroutine(DeathAnimation());
            //PlayerDied.Invoke();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(2);
        PlayerDied.Invoke();
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
        ApplyCollision(other.gameObject);
    }
    public static float GetMedian(float[] sourceArray, bool cloneArray = true)
    {
        //Framework 2.0 version of this method. there is an easier way in F4        
        if (sourceArray == null || sourceArray.Length == 0)
            throw new ArgumentException("Median of empty array not defined.");

        //make sure the list is sorted, but use a new array
        var sortedArray = cloneArray ? (float[])sourceArray.Clone() : sourceArray;
        Array.Sort(sortedArray);

        //get the median
        int size = sortedArray.Length;
        int mid = size / 2;
        if (size % 2 != 0)
            return sortedArray[mid];

        float value1 = sortedArray[mid];
        float value2 = sortedArray[mid - 1];
        return (sortedArray[mid] + value2) * 0.5f;
    }

    private List<float> angles = new List<float>();
    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(FireRateSeconds);
            if (GameIsRunning)
            {
                if (BulletCount == 1)
                {
                    Instantiate(Bullet, FirePosition.transform.position, Quaternion.identity);
                }
                else
                {
                    var angleOffset = 45f / BulletCount;
                    angles.Clear();
                    for (int i = 0; i < BulletCount; i++)
                    {
                        angles.Add(angleOffset * (i+1));
                    }

                    var medianOffset = GetMedian(angles.ToArray());

                    for (int i = 0; i < BulletCount; i++)
                    {
                        Instantiate(Bullet, FirePosition.transform.position, Quaternion.AngleAxis(angles[i] - medianOffset, Vector3.forward));
                    }
                }
                if (GunSound != null)
                    GunSound?.Play(0);
            }
        }
    }
    // Update is called once per frame
    public void Update()
    {
        //if (input.Player.Jump.ReadValue<float>() > 0)
        //{
        //}

        if (Time.timeScale > 0 && GameIsRunning && !IsDead)
        {
            Score++;
            text.text = "Score: " + Score;
        }
    }
}