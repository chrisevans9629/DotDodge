using UnityEngine;

namespace Assets.Scripts
{
    public class SwitchCamera : MonoBehaviour
    {
        public CameraFollow Follow;
        public Camera Camera;
        public Vector3 Position2D;
        public Vector3 Position3D;
        public bool Is3D;
        public void SwitchTo3D()
        {
            Camera.fieldOfView = 80;
            Camera.orthographic = false;

           // Camera.transform.position = Position3D;
           // Camera.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);

            LeanTween.value(gameObject, vector3 => Camera.transform.position = vector3, Camera.transform.position,
                Position3D, 1);
            LeanTween.value(gameObject, f => Camera.transform.rotation = Quaternion.AngleAxis(f, Vector3.up),
                Camera.transform.rotation.eulerAngles.y, 90, 1).setOnComplete(() =>
            {
                Follow.IsFollowingActive = true;
                Follow.SetupOffset();
            });

            
        }

        public void SwitchTo2D()
        {
            Follow.IsFollowingActive = false;
            //Camera.transform.position = Position2D;
            //Camera.transform.rotation = Quaternion.identity;
            //Camera.orthographic = true;
            LeanTween.value(gameObject, vector3 => Camera.transform.position = vector3, Camera.transform.position,
                Position2D, 1);
            LeanTween.value(gameObject, f => Camera.transform.rotation = Quaternion.AngleAxis(f, Vector3.up),
                Camera.transform.rotation.eulerAngles.y, 0, 1).setOnComplete(() => { Camera.orthographic = true; });
        }

        public void Toggle()
        {
            if (Is3D)
            {
                SwitchTo2D();
            }
            else
            {
                SwitchTo3D();
            }

            Is3D = !Is3D;
        }
    }
}