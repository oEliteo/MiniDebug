using System;
using System.Collections.Generic;
using System.Text;
using InControl;
using UnityEngine;

namespace MiniDebug
{
    public partial class MiniDebug
    {
        #region GUI

        private bool _show = true;
        private GUIStyle? _headerStyle, _bodyStyle, _boxBackground;


        private void OnGUI()
        {
            if (!_active || !_show) return;

            SetGuiStyles();

            // Set Up Info Display for mod in top right
            float width = 240f;
            float margin = 20f;
            float x = Screen.width - width - margin;
            float y = margin;

            bool began = false;
            try
            {
                // Give the area a tall but finite height; content will be sized by the vertical box
                GUILayout.BeginArea(new Rect(x, y, width, 1000f));
                began = true;

                // Draw a box that sizes to its contents
                GUILayout.BeginVertical(_boxBackground);
                GUILayout.Label("Mini-Debug Mod", _headerStyle);
                GUILayout.Label("Toggle Visibility: F2", _bodyStyle);
                GUILayout.Space(4);
                GUILayout.Label(
                    $"Position: X:{_position.x:F2}, Y:{_position.y:F2}\n" +
                    $"Scene: {_sceneName}",
                    _bodyStyle
                );
                GUILayout.EndVertical();
            }
            finally
            {
                if (began) GUILayout.EndArea();
            }
        }

        private void SetGuiStyles()
        {
            _headerStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperLeft,
                normal = { textColor = Color.white, background = null },
                padding = new RectOffset(0, 0, 0, 0)
            };

            _bodyStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 14,
                alignment = TextAnchor.UpperLeft,
                wordWrap = true,
                padding = new RectOffset(0, 0, 0, 0),
                normal = { background = null }
            };

            _boxBackground = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(10, 10, 8, 8)
            };
        }
        #endregion
    }
}
