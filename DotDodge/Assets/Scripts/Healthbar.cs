using UnityEngine;
using System.Linq;
namespace Assets.Scripts
{
    public class Healthbar : MonoBehaviour
    {
        public GameObject HealthItem;
        public Vector3 Offset;

        public void AddHealth()
        {
            if (transform.childCount > 0)
            {
                var item = transform.GetChild(transform.childCount - 1);
                Instantiate(HealthItem, item.position + Offset, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(HealthItem, transform.position, Quaternion.identity, transform);
            }
        }

        public void RemoveHealth()
        {
            if (transform.childCount > 0)
            {
                var item = transform.GetChild(transform.childCount - 1);
                Destroy(item.gameObject);
            }
        }

        public void ResetHealth()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}