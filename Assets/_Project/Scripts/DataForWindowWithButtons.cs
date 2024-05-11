using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataForWindowWithButtons", menuName = "Our scriptable objects", order = 51)]
public class DataForWindowWithButtons : ScriptableObject
{
    [SerializeField]
    public Sprite sprite;
    [SerializeField]
    public bool showSprite = true;
    [SerializeField] [TextAreaAttribute(2, 7)]
    public string mainText;
    [SerializeField]
    public List<string> textForButtons;
    [SerializeField]
    public bool goToNextScene = false;
}
