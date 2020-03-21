using System.Linq;
using UnityEditor;

namespace Assets.Editor
{
    public class BuildApp
    {
        [MenuItem("BuildApp/Switch To Plague Dodge")]
        public static void SwitchToPlagueDodge()
        {
            Switch(false,true);
            
            
        }

        private static void Switch(bool dotDodge, bool plagueDodge)
        {
            var array = EditorBuildSettings.scenes.ToArray();
            foreach (var editorBuildSettingsScene in array)
            {
                if (editorBuildSettingsScene.path.Contains("Scenes/DotDodge"))
                {
                    editorBuildSettingsScene.enabled = dotDodge;
                }
                else if (editorBuildSettingsScene.path.Contains("Scenes/PlagueDodge"))
                {
                    editorBuildSettingsScene.enabled = plagueDodge;
                }
            }
            EditorBuildSettings.scenes = array;
        }

        [MenuItem("BuildApp/Switch To DotDodge")]
        public static void SwitchToDotDodge()
        {
            Switch(true,false);
        }
    }
}