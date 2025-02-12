using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimelineUp/CharacterSO", fileName = "CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public List<Sprite> sprites;

    public Sprite GetCharacterSprite(int index)
    {
        return sprites[index];
    }
}
