using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimelineUp/ProjectileSO", fileName = "ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    public List<Sprite> sprites;

    public Sprite GetCharacterSprite(int index)
    {
        return sprites[index];
    }
}
