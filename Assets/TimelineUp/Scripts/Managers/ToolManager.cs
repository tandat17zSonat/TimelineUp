using Base.Singleton;
using SonatFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
    protected override void OnAwake()
    {
        PanelManager.Instance.OpenPanel<UITools>();
    }


}
