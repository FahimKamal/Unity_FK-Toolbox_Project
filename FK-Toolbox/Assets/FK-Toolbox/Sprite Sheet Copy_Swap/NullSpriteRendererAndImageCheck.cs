using System.Collections.Generic;
using Custom_Attribute;
using UnityEngine;

namespace FK_Toolbox
{
    /// <summary>
    /// Will select all gameObjects either with SpriteRender for Image component and put them in respective list.
    /// Also will separate gameObjects that has sprite missing and put them in separate list. 
    /// </summary>
    public class NullSpriteRendererAndImageCheck : MonoBehaviour
    {
        [SerializeField, ShowOnlyFK] private List<GameObject> nullSpriteRendererGO = new List<GameObject>();
        [SerializeField, ShowOnlyFK] private List<GameObject> nullImageGO = new List<GameObject>();

        [SerializeField] private List<GameObject> allGameObjects = new List<GameObject>();

        [EasyButtons.Button]
        void GetAllGameObjectsInScene()
        {
            // Get all gameObjects in the scene and put them in list
            allGameObjects.Clear();
            allGameObjects.AddRange(GameObject.FindObjectsOfType<GameObject>());
        
            // Go through the list if element has spriteRenderer component then put it in nullspriteRenderGo and if element has Image component then put it in nullImageGo list.
            foreach (var gameObject in allGameObjects)
            {
                if (gameObject.GetComponent<SpriteRenderer>())
                {
                    if (gameObject.GetComponent<SpriteRenderer>().sprite == null)
                    {
                        nullSpriteRendererGO.Add(gameObject);
                    }
                }

                if (gameObject.GetComponent<UnityEngine.UI.Image>())
                {
                    if (gameObject.GetComponent<UnityEngine.UI.Image>().sprite == null)
                    {
                        nullImageGO.Add(gameObject);
                    }
                }
            }

        }
    
        [EasyButtons.Button]
        void GetAllGameObjectsInPrefab()
        {
            // Get all gameObjects in the scene and put them in list
            allGameObjects.Clear();
            allGameObjects.AddRange(GetAllChilds(transform));
        
            // Go through the list if element has spriteRenderer component then put it in nullspriteRenderGo and if element has Image component then put it in nullImageGo list.
            foreach (var gameObject in allGameObjects)
            {
                if (gameObject.GetComponent<SpriteRenderer>())
                {
                    if (gameObject.GetComponent<SpriteRenderer>().sprite == null)
                    {
                        nullSpriteRendererGO.Add(gameObject);
                    }
                }

                if (gameObject.GetComponent<UnityEngine.UI.Image>())
                {
                    if (gameObject.GetComponent<UnityEngine.UI.Image>().sprite == null)
                    {
                        nullImageGO.Add(gameObject);
                    }
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
    }
}
