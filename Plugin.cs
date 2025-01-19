using UnityEngine;
using MelonLoader;

[assembly: MelonInfo(typeof(AspectRatioControlMod.AspectRatioControlMod), "AspectRatioControlMod", "0.0.1", "FieryWolfLevi")]
namespace AspectRatioControlMod
{
    public class AspectRatioControlMod : MelonMod
    {
        private static bool showSettings = true;
        private static int width = 1920;
        private static int height = 1080;
        private static bool isFullscreen = false;
        private static int refreshRate = 60;
        private static bool applyPeriodically = false;
        private static float lastAppliedTime = 0f;
        private static float applyInterval = 5f;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            MelonLogger.Msg("AspectRatioControlMod Loaded");
        }

        public override void OnGUI()
        {
            if (showSettings)
            {
                GUI.skin.button.normal.textColor = Color.white;
                GUI.skin.button.hover.textColor = Color.white;
                GUI.skin.button.fontSize = 14;

                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
                GUI.contentColor = Color.white;

                GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300), "Display Settings", GUI.skin.window);

                GUILayout.Label("Configure Screen Settings (Toggle F1)", GUILayout.ExpandWidth(true));

                GUILayout.Label("Width:");
                width = Mathf.Max(1, int.Parse(GUILayout.TextField(width.ToString(), GUILayout.Width(200))));

                GUILayout.Label("Height:");
                height = Mathf.Max(1, int.Parse(GUILayout.TextField(height.ToString(), GUILayout.Width(200))));

                isFullscreen = GUILayout.Toggle(isFullscreen, "Fullscreen");

                GUILayout.Label("Refresh Rate (Hz):");
                refreshRate = Mathf.Max(1, int.Parse(GUILayout.TextField(refreshRate.ToString(), GUILayout.Width(200))));

                applyPeriodically = GUILayout.Toggle(applyPeriodically, "Apply Periodically");

                if (GUILayout.Button("Set"))
                {
                    SetScreenSettings();
                }

                GUILayout.EndArea();
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                showSettings = !showSettings;
            }

            if (applyPeriodically && Time.time - lastAppliedTime >= applyInterval)
            {
                SetScreenSettings();
                lastAppliedTime = Time.time;
            }
        }

        private void SetScreenSettings()
        {
            Screen.SetResolution(width, height, isFullscreen, refreshRate);
        }
    }
}
