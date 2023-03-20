#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Custom_Attribute
{
    public static class PostInstallRun
    {
        static string GetPackageFullPath()
        {
            var packagePath = Path.GetFullPath("Packages/com.unity.textmeshpro");
            return Directory.Exists(packagePath) ? packagePath : null;
        }
        
        [InitializeOnLoadMethod]
        public static void RunAfterInstallPackage()
        {
            if (!Directory.Exists("Assets/FK-Toolbox"))
            {
                // var packageFullPath = GetPackageFullPath();
                // AssetDatabase.ImportPackage(packageFullPath + "/Package Resources/FK-Toolbox-package.unitypackage", true);

                var importResource = new ImportResource();
            }
        }
    }

    public class ImportResource : EditorWindow
    {
        private void OnEnable()
        {
            SetEditorWindowSize();
        }

        private void OnGUI()
        {
            titleContent = new GUIContent("FK-Toolbox Resource Importer");
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

