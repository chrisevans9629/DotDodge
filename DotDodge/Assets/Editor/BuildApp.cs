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
            var packageName = "com.evanssoftware.plaguedodge";
            var productName = "Plague Dodge";
            UpdateBuildSettings(packageName, productName);
        }
        [MenuItem("BuildApp/Switch To DotDodge")]
        public static void SwitchToDotDodge()
        {
            Switch(true, false);
            UpdateBuildSettings("com.evanssoftware.dotdodge", "Dot Dodge");
        }

        [MenuItem("BuildApp/Increment Version")]
        public static void IncrementVersion()
        {
            PlayerSettings.Android.bundleVersionCode++;
            PlayerSettings.bundleVersion = "0." + PlayerSettings.Android.bundleVersionCode;
        }

        [MenuItem("BuildApp/Build")]
        public static void Build()
        {
           // BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, )
        }

        private static void UpdateBuildSettings(string packageName, string productName)
        {
            PlayerSettings.productName = productName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, packageName);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, packageName);
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

       
    }
}