using System.IO;
using UnityEditor;

using UnityEngine;

namespace Event_System_SO.Editor
{
    [InitializeOnLoad]
    public class MyPackagePostProcessor : EditorWindow
    {
        // [SerializeField]
        // TMP_PackageResourceImporter m_ResourceImporter;
        //
        // static TMP_PackageResourceImporterWindow m_ImporterWindow;

        public static void ShowPackageImporterWindow()
        {
            // if (m_ImporterWindow == null)
            // {
            //     m_ImporterWindow = GetWindow<TMP_PackageResourceImporterWindow>();
            //     m_ImporterWindow.titleContent = new GUIContent("TMP Importer");
            //     m_ImporterWindow.Focus();
            // }
        }

        void OnEnable()
        {
            // Set Editor Window Size
            SetEditorWindowSize();

            // if (m_ResourceImporter == null)
            //     m_ResourceImporter = new TMP_PackageResourceImporter();
            //
            // if (m_ResourceImporter.m_IsImportingExamples)
            //     m_ResourceImporter.RegisterResourceImportCallback();
        }

        void OnDestroy()
        {
            // m_ResourceImporter.OnDestroy();
        }

        void OnGUI()
        {
            // m_ResourceImporter.OnGUI();
            // Check if the resources state has changed.
            var m_EssentialResourcesImported = File.Exists("Assets/TextMesh Pro/Resources/TMP Settings.asset");
            var m_ExamplesAndExtrasResourcesImported = Directory.Exists("Assets/TextMesh Pro/Examples & Extras");

            GUILayout.BeginVertical();
            {
                // Display options to import Essential resources
                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    GUILayout.Label("TMP Essentials", EditorStyles.boldLabel);
                    GUILayout.Label("This appears to be the first time you access TextMesh Pro, as such we need to add resources to your project that are essential for using TextMesh Pro. These new resources will be placed at the root of your project in the \"TextMesh Pro\" folder.", new GUIStyle(EditorStyles.label) { wordWrap = true } );
                    GUILayout.Space(5f);

                    GUI.enabled = !m_EssentialResourcesImported;
                    if (GUILayout.Button("Import TMP Essentials"))
                    {
                        AssetDatabase.importPackageCompleted += ImportCallback;

                        // string packageFullPath = GetPackageFullPath();
                        //AssetDatabase.ImportPackage(packageFullPath + "/Package Resources/TMP Essential Resources.unitypackage", false);
                    }
                    GUILayout.Space(5f);
                    GUI.enabled = true;
                }
                GUILayout.EndVertical();

                // Display options to import Examples & Extras
                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    GUILayout.Label("TMP Examples & Extras", EditorStyles.boldLabel);
                    GUILayout.Label("The Examples & Extras package contains addition resources and examples that will make discovering and learning about TextMesh Pro's powerful features easier. These additional resources will be placed in the same folder as the TMP essential resources.", new GUIStyle(EditorStyles.label) { wordWrap = true });
                    GUILayout.Space(5f);

                    GUI.enabled = m_EssentialResourcesImported && !m_ExamplesAndExtrasResourcesImported;
                    if (GUILayout.Button("Import TMP Examples & Extras"))
                    {
                        // Set flag to get around importing scripts as per of this package which results in an assembly reload which in turn prevents / clears any callbacks.
                       // m_IsImportingExamples = true;

                        // Disable AssetDatabase refresh until examples have been imported.
                        //AssetDatabase.DisallowAutoRefresh();

                        //string packageFullPath = GetPackageFullPath();
                        //AssetDatabase.ImportPackage(packageFullPath + "/Package Resources/TMP Examples & Extras.unitypackage", false);
                    }
                    GUILayout.Space(5f);
                    GUI.enabled = true;
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();
            GUILayout.Space(5f);
        }

        private void ImportCallback(string packagename)
        {
            
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }

        /// <summary>
        /// Limits the minimum size of the editor window.
        /// </summary>
        void SetEditorWindowSize()
        {
            EditorWindow editorWindow = this;

            Vector2 windowSize = new Vector2(640, 210);
            editorWindow.minSize = windowSize;
            editorWindow.maxSize = windowSize;
        }
    }
}
