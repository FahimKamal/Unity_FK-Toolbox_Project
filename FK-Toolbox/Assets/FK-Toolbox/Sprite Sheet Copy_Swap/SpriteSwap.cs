using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FK_Toolbox
{
    public class SpriteSwap : MonoBehaviour
    {
        [SerializeField] private Texture2D oldTexture;
        [SerializeField] private Texture2D newTexture;
        [SerializeField] private List<Sprite> oldSprites;
        [SerializeField] private List<Sprite> newSprites;

        [Multiline] [SerializeField] private string description;

        [SerializeField] private bool swapPossible;

        [SerializeField] private List<GameObjectAndSpriteHolder> gameObjectsWithSprites;
        [SerializeField] private List<GameObjectAndSpriteHolder> gameObjectsWithProblem;
        [SerializeField] private List<GameObjectAndSpriteHolder> gameObjectsUnchanged;

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
                    if (item is Sprite)
                    {
                        oldSprites.Add(item as Sprite);
                    }
                }
            }

            var newTextureData = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(newTexture));
            if (newSprites.Count == 0)
            {
                foreach (var item in newTextureData)
                {
                    if (item is Sprite)
                    {
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
        void GetAllGameObjectWithSprite()
        {
            gameObjectsWithSprites.Clear();

            var gameObjects = GetAllChilds(gameObject.transform);
            foreach (var gameObject in gameObjects)
            {
                var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    var gameObjectAndSpriteHolder = new GameObjectAndSpriteHolder();
                    gameObjectAndSpriteHolder.gameObject = gameObject;
                    gameObjectAndSpriteHolder.sprite = spriteRenderer.sprite;
                    gameObjectsWithSprites.Add(gameObjectAndSpriteHolder);
                }
            }

            EditorUtility.SetDirty(this);
        }

        [EasyButtons.Button]
        void ClearGameObjectList()
        {
            gameObjectsWithSprites.Clear();
            gameObjectsWithProblem.Clear();
            gameObjectsUnchanged.Clear();
            description = "GameObject List Cleared.";
            EditorUtility.SetDirty(this);
        }


        [EasyButtons.Button]
        void CheckGameObjects()
        {
            if (gameObjectsWithSprites.Count == 0)
            {
                description = "List is empty";
                return;
            }

            gameObjectsWithProblem.Clear();
            gameObjectsUnchanged.Clear();

            foreach (var obj in gameObjectsWithSprites)
            {
                // check if the attached SpriteRender has any sprite or not
                var spriteRenderer = obj.gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer.sprite == null)
                {
                    gameObjectsWithProblem.Add(obj);
                    Debug.Log("SpriteRenderer is not attached to " + obj.gameObject.name);
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
            if (!swapPossible || !(gameObjectsWithSprites.Count > 0))
            {
                return;
            }

            // Now swap all sprites of GameObject from list gameObjectsWithSprites with newSprites list. 
            foreach (var gameObjectAndSpriteHolder in gameObjectsWithSprites)
            {
                var spriteRenderer = gameObjectAndSpriteHolder.gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    if (gameObjectAndSpriteHolder.sprite == null)
                    {
                        continue;
                    }

                    var index = newSprites.FindIndex(x => x.name == gameObjectAndSpriteHolder.sprite.name);
                    if (index != -1)
                    {
                        spriteRenderer.sprite = newSprites[index];
                        if (gameObjectsUnchanged.Contains(gameObjectAndSpriteHolder))
                        {
                            gameObjectsUnchanged.Remove(gameObjectAndSpriteHolder);
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
    class GameObjectAndSpriteHolder
    {
        public GameObject gameObject;
        public Sprite sprite;
    }
}