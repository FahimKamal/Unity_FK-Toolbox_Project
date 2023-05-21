using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FK_Toolbox
{
    /// <summary>
    /// Will be attached with a parent object. Will select all child gameObject with Image component.
    /// Will swap the sprite of the Image component of the child gameObject with the sprite from given texture.
    /// </summary>
    public class CanvasImageSwap : MonoBehaviour
    {
        [SerializeField] private Texture2D oldTexture;
        [SerializeField] private Texture2D newTexture;
        [SerializeField] private List<Sprite> oldSprites;
        [SerializeField] private List<Sprite> newSprites;
    
        [Multiline]
        [SerializeField] private string description;
    
        [SerializeField] private bool swapPossible;
    
        [SerializeField] private List<GameObjectAndImageHolder> gameObjectsWithImages;
        [SerializeField] private List<GameObjectAndImageHolder> gameObjectsWithProblem;
        [SerializeField] private List<GameObjectAndImageHolder> gameObjectsUnchanged;
        // Start is called before the first frame update
        void Start()
        {
        
        }

#if UNITY_EDITOR
        /// <summary>
        /// Load all sprites from given textures.
        /// </summary>
        [EasyButtons.Button]
        private void GetSprites()
        {
            var oldTextureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(oldTexture));
            if (oldSprites.Count == 0)
            {
                foreach (var item in oldTextureData)
                {
                    if (item is Sprite){
                        oldSprites.Add(item as Sprite);
                    }
                }
            }

            var newTextureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(newTexture));
            if (newSprites.Count == 0)
            {
                foreach (var item in newTextureData)
                {
                    if (item is Sprite){
                        newSprites.Add(item as Sprite);
                    }
                }
            }
            EditorUtility.SetDirty(this);
        }

    
        [EasyButtons.Button]
        private void CompareBothSpriteList()
        {
            if (oldSprites.Count != newSprites.Count)
            {
                description = "Number of sprites in both list is not same.";
                return;
            }
            else
            {
                description = "Number of sprites in both list is same.";
            }

            // Make a new copy of list newSprites in a local variable
            var newSpriteListCopy = new List<Sprite>();
            newSpriteListCopy.AddRange(newSprites);

            bool spriteDifference = false;
            foreach (var sprite in oldSprites)
            { 
                // Check if sprite.name is present in newSprites list.
                var spriteName = sprite.name;

                var index = newSpriteListCopy.FindIndex(x => x.name == spriteName);
                if (index == -1)
                {
                    spriteDifference = true;
                    break;
                }
                else
                {
                    newSpriteListCopy.RemoveAt(index);
                }
            }

            if (spriteDifference)
            {
                description += "\nSprite difference found.";
            }
            else
            {
                description += "\nBoth List has same sprites";
                swapPossible = true;
            }
        }

        [EasyButtons.Button]
        void ClearBothSpriteList()
        {
            oldSprites.Clear();
            newSprites.Clear();
            swapPossible = false;
            description = "Both Sprite List Cleared.";
            EditorUtility.SetDirty(this);
        }

    

        [EasyButtons.Button]
        void GetAllGameObjectWithImage()
        {
            gameObjectsWithImages.Clear();

            var gameObjects = GetAllChilds(gameObject.transform);
            foreach (var gameObj in gameObjects)
            {
                var image = gameObj.GetComponent<Image>();
                if (image != null)
                {
                    var gameObjectAndImageHolder = new GameObjectAndImageHolder
                    {
                        gameObject = gameObj,
                        image = image
                    };
                    gameObjectsWithImages.Add(gameObjectAndImageHolder);
                }
            }
            EditorUtility.SetDirty(this);
        }

        [EasyButtons.Button]
        void ClearGameObjectList()
        {
            gameObjectsWithImages.Clear();
            gameObjectsWithProblem.Clear();
            gameObjectsUnchanged.Clear();
            description = "GameObject List Cleared.";
            EditorUtility.SetDirty(this);
        }

    
        [EasyButtons.Button]
        void CheckGameObjects()
        {
            if (gameObjectsWithImages.Count == 0)
            {
                description = "List is empty";
                return;
            }
        
            gameObjectsWithProblem.Clear();
            gameObjectsUnchanged.Clear();

            foreach (var obj in gameObjectsWithImages)
            {
                // check if the attached SpriteRender has any sprite or not
                var image = obj.gameObject.GetComponent<Image>();
                if (image.sprite == null)
                {
                    gameObjectsWithProblem.Add(obj);
                    Debug.Log( "SpriteRenderer is not attached to " + obj.gameObject.name);
                }
                else
                {
                    gameObjectsUnchanged.Add(obj);
                }
            }
        
        }


        [EasyButtons.Button]
        void SwapSprites()
        {
            if (!swapPossible || !(gameObjectsWithImages.Count > 0))
            {
                return;
            }

            // Now swap all sprites of GameObject from list gameObjectsWithImages with newSprites list. 
            foreach (var gameObjectAndImageHolder in gameObjectsWithImages)
            {
                var image = gameObjectAndImageHolder.gameObject.GetComponent<Image>();
                if (image != null)
                {
                    if (image.sprite == null)
                    {
                        continue;
                    }
                    var index = newSprites.FindIndex(x => x.name == gameObjectAndImageHolder.image.sprite.name);
                    if (index != -1)
                    {
                        image.sprite = newSprites[index];
                        Debug.Log("Sprite Swapped for " + gameObjectAndImageHolder.gameObject.name);
                        if(gameObjectsUnchanged.Contains(gameObjectAndImageHolder))
                        {
                            gameObjectsUnchanged.Remove(gameObjectAndImageHolder);
                        }
                    }
                }
            }
            EditorUtility.SetDirty(this);
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

#endif
    }

    [Serializable]
    class GameObjectAndImageHolder
    {
        public GameObject gameObject;
        public Image image;
    }
}