using System;
using System.Collections.Generic;
using System.IO;
using Custom_Attribute;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace FK_Toolbox
{
    /// <summary>
    /// A Reference texture will be given. Program will find all other textures from that directory and sub-directory.
    /// Then it will check all textures from list and find textures that has multiple sprites of same name in it.
    /// So I can make all sprite with unique name. 
    /// </summary>
    public class SpriteUniqueNameCheck : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Texture2D referenceTexture;
        [SerializeField, ShowOnlyFK] private List<Texture2D> allTexturesInRefLocation;
        [SerializeField, ShowOnlyFK] private List<SpriteListWithCommonName> spriteListWithCommonNames;
        [Button]
        void GetAllTextureAtRefLocation()
        {
            var path = AssetDatabase.GetAssetPath(referenceTexture);
            // separate file name from path
            var fileName = System.IO.Path.GetFileName(path);
            Debug.Log(fileName);

            // separate only the path from filename
            var filePath = path.Substring(0, path.Length - fileName.Length);
            Debug.Log(filePath);

            if (referenceTexture == null)
            {
                Debug.LogError("No texture found");
                return;
            }

            var fileInfo = new DirectoryInfo(filePath);
            if (allTexturesInRefLocation.Count > 0)
            {
                allTexturesInRefLocation.Clear();
            }

            foreach (var data in fileInfo.GetFiles())
            {
                // if data is of texture2D type put them in list
                if (data.Extension == ".png" || data.Extension == ".jpg")
                {
                    allTexturesInRefLocation.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(filePath + data.Name));
                }
            }
            
            // Goto All subdirectories and find all textures from those locations as well.
            foreach (var data in fileInfo.GetDirectories())
            {
                foreach (var data2 in data.GetFiles()){
                    if (data2.Extension == ".png" || data2.Extension == ".jpg")
                    {
                        allTexturesInRefLocation.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(filePath + data.Name + "/" + data2.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Check all Textures from list and find textures that has multiple sprites of same name in it.
        /// </summary>
        [Button]
        private void CheckAllTexture()
        {
            if (spriteListWithCommonNames.Count > 0)
            {
                spriteListWithCommonNames.Clear();
            }

            foreach (var tex in allTexturesInRefLocation)
            {
                var textureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(tex));

                Dictionary<string, List<Sprite>> spriteList = new Dictionary<string, List<Sprite>>();
                foreach (var sp in textureData)
                {
                    if (sp is Sprite)
                    {
                        if (spriteList.ContainsKey(sp.name))
                        {
                            spriteList[sp.name].Add(sp as Sprite);
                        }
                        else
                        {
                            spriteList.Add(sp.name, new List<Sprite> { sp as Sprite });
                        }
                    }
                }

                // Now check the list and if any key is found with multiple sprite them put that texture, name and sprite
                // in SpriteListWithCommonName class and create a list of those object.

                foreach (var item in spriteList)
                {
                    if (item.Value.Count > 1)
                    {
                        Debug.Log("Found one");
                        Debug.Log(tex.name);
                        Debug.Log(item.Key);
                        SpriteListWithCommonName spriteListWithCommonName = new SpriteListWithCommonName
                        {
                            textureName = tex.name,
                            texture = tex,
                            spriteName = item.Key,
                            spriteList = item.Value
                        };
                        spriteListWithCommonNames.Add(spriteListWithCommonName);
                        EditorUtility.SetDirty(this);
                    }
                }
            }
        }
#endif
    }

    [Serializable]
    public class SpriteListWithCommonName
    {
        [ShowOnlyFK]
        public string textureName;
        [ShowOnlyFK]
        public Texture2D texture;
        [ShowOnlyFK]
        public string spriteName;
        [ShowOnlyFK]
        public List<Sprite> spriteList;
    }
}