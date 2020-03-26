using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillWall : MonoBehaviour
{
    public string[] IgnoreTags;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger 2d hit", this);
        if (IgnoreTags != null && IgnoreTags.Contains(collision.tag))
        {
            return;
        }
        Destroy(collision.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }


}
