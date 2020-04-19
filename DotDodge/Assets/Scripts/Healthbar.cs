using UnityEngine;
using System.Linq;
namespace Assets.Scripts
{
    public class Healthbar : MonoBehaviour
    {
        public GameObject HealthItem;
        public Vector3 Offset;
        private int Count => transform.childCount;
        public void AddHealth()
        {
            Debug.Log($"Add health: count={Count}", this);
            if (Count > 0)
            {
                var item = transform.GetChild(Count - 1);
                Instantiate(HealthItem, item.position + Offset, Quaternion.identity, transform);
                //Count++;
            }
            else
            {
                Instantiate(HealthItem, transform.position, Quaternion.identity, transform);
                //Count++;
            }
        }

        public void RemoveHealth()
        {
            if (Count > 0)
            {
                var item = transform.GetChild(Count - 1);
                Destroy(item.gameObject);
            }
        }

        public void ResetHealth(int count)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            Vector3 position = transform.position;
            for (int i = 0; i < count; i++)
            {
                var result = Instantiate(HealthItem, position + Offset, Quaternion.identity, transform);
                position = result.transform.position;
            }

            Debug.Log("Reset health",this);
            //Count = 0;
        }
    }
}