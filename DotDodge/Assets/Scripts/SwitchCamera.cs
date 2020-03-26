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
            Follow.IsFollowingActive = true;
            Camera.transform.position = Position3D;
            Camera.orthographic = false;
            Camera.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            Follow.SetupOffset();
        }

        public void SwitchTo2D()
        {
            Follow.IsFollowingActive = false;
            Camera.transform.position = Position2D;
            Camera.transform.rotation = Quaternion.identity;
            Camera.orthographic = true;
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