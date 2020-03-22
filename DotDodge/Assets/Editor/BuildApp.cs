using System.Linq;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
namespace Assets.Editor
{
    public class BuildApp : EditorWindow
    {
        [MenuItem("BuildApp/Open Settings")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<BuildApp>();
        }

        [MenuItem("BuildApp/Switch To Coronaforce")]
        public static void SwitchToPlagueDodge()
        {
            Switch(false,true);
            var packageName = "com.evanssoftware.coronaforce";
            var productName = "Coronaforce";
            UpdateBuildSettings(packageName, productName,"PlagueDodge");
        }
        [MenuItem("BuildApp/Switch To DotDodge")]
        public static void SwitchToDotDodge()
        {
            Switch(true, false);
            UpdateBuildSettings("com.evanssoftware.dotdodge", "Dot Dodge", "DotDodge");
        }

        [MenuItem("BuildApp/Increment Version")]
        public static void IncrementVersion()
        {
            PlayerSettings.Android.bundleVersionCode++;
            PlayerSettings.bundleVersion = "0." + PlayerSettings.Android.bundleVersionCode;
        }

        private static void UpdateBuildSettings(string packageName, string productName, string iconFolder)
        {
            PlayerSettings.productName = productName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, packageName);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, packageName);

            var result = PlayerSettings.GetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Adaptive);

            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Screenshots/{iconFolder}/icon.png");

            result.First().SetTextures(new []{texture,texture});
            //result.SetTexture();

            var result2 = PlayerSettings.GetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy);

            result2.First().SetTexture(texture);

            var result3 = PlayerSettings.GetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Round);
            result3.First().SetTexture(texture);


            PlayerSettings.SetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Adaptive, result);
            PlayerSettings.SetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy, result2);
            PlayerSettings.SetPlatformIcons(BuildTargetGroup.Android, AndroidPlatformIconKind.Round, result3);
            //PlayerSettings.SetPlatformIcons(BuildTargetGroup.Android, null, new []{new PlatformIcon(), });
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
#endif