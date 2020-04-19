using UnityEngine;
using System.Linq;
namespace Assets.Scripts
{
    public class Healthbar : MonoBehaviour
    {
        public GameObject HealthItem;
        public Vector3 Offset;
        private int Count = 0;
        public void AddHealth()
        {
            if (Count > 0)
            {
                var item = transform.GetChild(transform.childCount - 1);
                Instantiate(HealthItem, item.position + Offset, Quaternion.identity, transform);
                Count++;
            }
            else
            {
                Instantiate(HealthItem, transform.position, Quaternion.identity, transform);
                Count++;
            }
        }

        public void RemoveHealth()
        {
            if (Count > 0)
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

            Count = 0;
        }
    }
}