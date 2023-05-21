/// This script is used to swap the sprites of a prefab with the sprites of another texture.

using System;
using System.Collections.Generic;
using System.IO;
using Custom_Attribute;
using UnityEditor;
using UnityEngine;

namespace FK_Toolbox
{
    /// <summary>
    /// Will be attached with a parent gameObject. Program will get all gameObject with spriteRenderer.
    /// User will have to give old texture spriteSheet also the new texture spriteSheet. This way user will be able to
    /// swap the old sprites with new given sprites. 
    /// </summary>
    public class SpriteSwap : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Texture2D referenceTexture;
        [SerializeField, ShowOnlyFK] private List<Texture2D> allTexturesInRefLocation;
        [SerializeField, ShowOnlyFK] private List<Texture2D> allTexturesInPrefab;
    
        [SerializeField] private Texture2D oldTexture;
        [SerializeField] private Texture2D newTexture;

        [SerializeField] private List<Sprite> _oldSprites;
        [SerializeField] private List<Sprite> _newSprites;
    
        [SerializeField, ShowOnlyFK] private List<GameObject> allSpriteRendererObjs = new List<GameObject>();

        [SerializeField, ShowOnlyFK] private List<TextureContainer> detectedTextures;

        [EasyButtons.Button]
        void GetSpritesFromOldTexture()
        {
            var textureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(oldTexture));

            foreach (var texture in textureData)
            {
                if (texture is Sprite)
                {
                    _oldSprites.Add(texture as Sprite);
                }
            }
            
        }
    
        [EasyButtons.Button]
        void GetSpritesFromNewTexture()
        {
            var textureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(newTexture));

            foreach (var texture in textureData)
            {
                if (texture is Sprite)
                {
                    _newSprites.Add(texture as Sprite);
                }
            }
            
        }

        [EasyButtons.Button]
        void GetAllGameObjectsInPrefab()
        {
            // Get all gameObjects in the scene and put them in list
            allSpriteRendererObjs.Clear();
            var allObjs = GetAllChilds(transform);
        
            // Go through the list if element has spriteRenderer component then put it in nullspriteRenderGo and if element has Image component then put it in nullImageGo list.
            foreach (var gameObject in allObjs)
            {
                if (gameObject.GetComponent<SpriteRenderer>())
                {
                    allSpriteRendererObjs.Add(gameObject);
                }
            }

        }
    
        List<GameObject> GetAllChilds(Transform _t)
        {
            List<GameObject> ts = new List<GameObject>();

            foreach (Transform t in _t)
            {
                ts.Add(t.gameObject);
                if (t.childCount > 0)
                    ts.AddRange(GetAllChilds(t));
            }

            return ts;
        }
    
        [EasyButtons.Button]
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
        
            // Go through each textures and get all sprites from them and put them in a dictionary. Where the key of dictionary will be the texture and the value will be a list of all sprites in that texture.
            detectedTextures = new List<TextureContainer>();
            foreach (var texture in allTexturesInRefLocation)
            {
                var textureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture));
                var textureContainer = new TextureContainer();
                textureContainer.textureName = texture.name;
                textureContainer.texture = texture;
                foreach (var data in textureData)
                {
                    if (data is Sprite)
                    {
                        var spriteAndObjectContainer = new SpriteAndObjectContainer();
                        spriteAndObjectContainer.spriteName = data.name;
                        spriteAndObjectContainer.sprite = data as Sprite;
                        textureContainer.spriteList.Add(spriteAndObjectContainer);
                    }
                }

                detectedTextures.Add(textureContainer);
            }
        }

        [EasyButtons.Button]
        private void DetectAllTextureUsedInPrefab()
        {
            if (allSpriteRendererObjs.Count  == 0)
            {
                Debug.LogError("No gameObjects in the scene");
                return;
            }
            // Go through all elements of allSpriteRendererObjs list and detect each textures used and put them in allTexturesInPrefab.
            allTexturesInPrefab.Clear();
            foreach (var gameObject in allSpriteRendererObjs)
            {
                var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer.sprite)
                {
                    Debug.Log("found one");
                    if (!allTexturesInPrefab.Contains(spriteRenderer.sprite.texture))
                    {
                        allTexturesInPrefab.Add(spriteRenderer.sprite.texture);
                    }
                
                }
            }
        }
    
        [EasyButtons.Button]
        void SwapSprite()
        {
            foreach (var spriteRendererObj in allSpriteRendererObjs)
            {
                var spriteRenderer = spriteRendererObj.GetComponent<SpriteRenderer>();
                var sprite = spriteRenderer.sprite;
                if (_oldSprites.Contains(sprite))
                {
                    Debug.Log("found one");
                    var newSp = _newSprites.Find(x => x.name == sprite.name);
                    spriteRenderer.sprite = newSp;
                }
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }

    [Serializable]
    public class TextureContainer
    {
        public string textureName;
        public Texture2D texture;
        public List<SpriteAndObjectContainer> spriteList = new List<SpriteAndObjectContainer>();
    }

    [Serializable]
    public class SpriteAndObjectContainer
    {
        public string spriteName;
        public Sprite sprite;
        public List<GameObject> spriteObj;
    }
}