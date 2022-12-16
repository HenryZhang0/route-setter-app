using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LaunchWallEditor()
    {
        SceneManager.LoadScene("WallEditorScene");
    }

    public void LoadNewClimb()
    {
        ClimbInfo.isNew = true;
        LaunchWallEditor();
    }
        

    public void LoadClimb(ClimbData data) 
    {
        ClimbInfo.climb = data;
        LaunchWallEditor();
    }

}

// ClimbInfo is passed through different scenes
public static class ClimbInfo 
{
    public static bool isNew = false;
    public static ClimbData climb { get; set; }
}