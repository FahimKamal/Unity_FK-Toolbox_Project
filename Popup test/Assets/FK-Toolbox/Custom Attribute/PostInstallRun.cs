/*
 * For code reference Read [TMP_PackageResourceImporter.cs] file from TextMeshPro package.
 */

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Custom_Attribute
{
    public static class PostInstallRun
    {
        [InitializeOnLoadMethod]
        public static void RunAfterInstallPackage()
        {
            if (!Directory.Exists("Assets/FK-Toolbox"))
            {
                Debug.Log("Assets no found");

                
                ImportResource.ShowImportResourceWindow();
            }
        }
    }

    public class ImportResource : EditorWindow
    {
        private static ImportResource _importResourceWindow;

        private bool _resourceImported;
        private void OnEnable()
        {
            SetEditorWindowSize();
        }
        
        static string GetPackageFullPath()
        {
            // var packagePath = Path.GetFullPath("C:/Users/Fahim Kamal/Documents/Unity Projects/Unity_Popup_Manager/Popup test/Assets/FK-Toolbox");
            var packagePath = Path.GetFullPath("Packages/com.novalabs.fk-tools");
            return Directory.Exists(packagePath) ? packagePath : null;
        }

        public static void ShowImportResourceWindow()
        {
            if (_importResourceWindow == null)
            {
                _importResourceWindow = GetWindow<ImportResource>();
                _importResourceWindow.titleContent = new GUIContent("FK-Toolbox Resource Importer");
                _importResourceWindow.Focus();
            }
        }

        private void OnGUI()
        {
            _resourceImported = Directory.Exists("Assets/FK-Toolbox");
            GUILayout.BeginVertical();
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    GUILayout.Label("FK-Toolbox Essentials", EditorStyles.boldLabel);
                    GUILayout.Label("This appears to be the first time you installed FK-Toolbox, as such we need to add resources to your project that are essential for using FK-Toolbox. These new resources will be placed at the root of your project in the \"FK-Toolbox\" folder.", new GUIStyle(EditorStyles.label) { wordWrap = true } );
                    GUILayout.Space(5f);
                    GUI.enabled = !_resourceImported;
                    if (GUILayout.Button("Import FK-Toolbox Essentials"))
                    {
                        
                        var packageFullPath = GetPackageFullPath();
                        Debug.Log(packageFullPath);
                        AssetDatabase.ImportPackage(packageFullPath + "/Package Resources/FK-Toolbox-package.unitypackage", true);
                    }
                    GUILayout.Space(5f);
                    GUI.enabled = true;
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
            
        }

        void SetEditorWindowSize()
        {
            EditorWindow editorWindow = this;

            Vector2 windowSize = new Vector2(640, 210);
            editorWindow.minSize = windowSize;
            editorWindow.maxSize = windowSize;
        }
    }
}
#endif

