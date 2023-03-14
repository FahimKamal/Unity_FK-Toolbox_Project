using System.Collections.Generic;
using Events_Scripts;
using UnityEngine;

public class TreeID : MonoBehaviour
{
    [SerializeField] private int ID = 0;

    [SerializeField] private Color skinColor;

    [SerializeField] private IntEvent _intEvent;
    
    [SerializeField]
    private List<SpriteRenderer> sp;


    private void OnCollisionEnter2D(Collision2D col)
    {
        _intEvent.RaiseEvent(ID);
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sp.Count > 0)
        {
            foreach (var VARIABLE in sp)
            {
                VARIABLE.color = skinColor;
            }
        }
    }
#endif
}
