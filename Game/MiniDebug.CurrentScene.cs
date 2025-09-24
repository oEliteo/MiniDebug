using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

namespace MiniDebug
{
    public partial class MiniDebug
    {
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"Scene Loaded: {scene.name}");
            _sceneName = scene.name;
            ReacquireHero();
        }

        private void GetCurrentScene()
        {
            _sceneName = SceneManager.GetActiveScene().name;
        }
    }
}
