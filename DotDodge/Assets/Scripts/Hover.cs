using UnityEngine;

namespace Assets.Scripts
{
    public class Hover : MonoBehaviour
    {
        public float Amount = 1;
        public float Duration = 2;
        void Start()
        {
            MoveUp();
        }

        private LTDescr current;
        void MoveUp()
        {
            if (hover)
                current = LeanTween.moveY(gameObject, transform.position.y + Amount, Duration).setEaseInOutQuad().setOnComplete(MoveDown);
        }

        void MoveDown()
        {
            if (hover) 
               current = LeanTween.moveY(gameObject, transform.position.y - Amount, Duration).setEaseInOutQuad().setOnComplete(MoveUp);
        }

        private bool hover = true;
        public void StopHovering()
        {
            hover = false;
            LeanTween.cancel(current.uniqueId);
        }
    }
}