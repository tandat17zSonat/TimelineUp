using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimelineUp/TimelineEraSO", fileName = "TimelineEraSO")]
public class TimelineEraSO : ScriptableObject
{
    public int TimelineId;
    public int EraId;

    public List<Sprite> entitySprites;
    public List<Sprite> projectileSprites;
    

}
