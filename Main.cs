using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniDebug
{
    [BepInPlugin("com.elite.mini.debug", "Mini Debug", "1.0.0")]
    public partial class MiniDebug : BaseUnityPlugin
    {
        #region initialization
        //mod variables
        private bool _active;
        private static bool _initialized;

        //potential state variables?
        private HeroController? _heroController;
        private Vector3 _position;
        private string? _sceneName;

        //BepinEx Script Engine Useful? Things?
        private const string HarmonyId = "com.elite.mini.debug";
        private Harmony? _harmony;

        #endregion


        #region bepinex
        private void Awake()
        {
            if (_initialized) Logger.LogInfo("MiniDebug Already Initialized...");
            if (_initialized) return;
            _initialized = true;

            _active = true;
            Logger.LogInfo("MiniDebug Initializing...");

            _harmony = new Harmony(HarmonyId);
            _harmony.PatchAll();

            SceneManager.sceneLoaded += OnSceneLoaded;

            ReacquireHero();
            _sceneName = SceneManager.GetActiveScene().name;
        }

        private void OnEnable()
        {
            Logger.LogInfo("MiniDebug Resuming...");

            _active = true;
            _show = true;
            _harmony?.PatchAll();

            SceneManager.sceneLoaded += OnSceneLoaded;
            ReacquireHero();
            _sceneName = SceneManager.GetActiveScene().name;
        }

        private void OnDisable()
        {
            Logger.LogInfo("MiniDebug Pausing...");

            // Small Cleanup and pause on disable.
            _heroController = null;
            _sceneName = null;
            _active = false;
            _show = false;

            SceneManager.sceneLoaded -= OnSceneLoaded;
            try { StopAllCoroutines(); } catch { }
            try { _harmony?.UnpatchSelf(); } catch { }
        }

        private void OnDestroy()
        {
            Logger.LogInfo("MiniDebug Cleaning Up...");

            // Cleanup & Unhook
            _heroController = null;
            _sceneName = null;
            _active = false;
            _show = false;
            _initialized = false;

            SceneManager.sceneLoaded -= OnSceneLoaded;
            try { StopAllCoroutines(); } catch { }
            try { _harmony?.UnpatchSelf(); } catch { }
        }

        private void Update()
        {
            if (!_active) return;

            // Toggle overlay with F2
            if (Input.GetKeyDown(KeyCode.F2))
                _show = !_show;

            if (_heroController == null)
            {
                ReacquireHero();
                return; // skip this frame if still null
            }

            _position = _heroController.transform.position;

            _sceneName = SceneManager.GetActiveScene().name;
        }
        #endregion
    }
}
