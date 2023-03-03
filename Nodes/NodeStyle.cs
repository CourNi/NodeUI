using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityTools.NodeUI
{
    public static class NodeStyle
    {
        public static GUIStyle Node
        {
            get
            {
                GUIStyle style = new();
                style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
                style.border = new RectOffset(12, 12, 12, 12);
                return style;
            }
        }

        public static GUIStyle NodeSelected
        {
            get
            {
                GUIStyle style = new();
                style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0 on.png") as Texture2D;
                style.border = new RectOffset(12, 12, 12, 12);
                return style;
            }
        }

        public static GUIStyle InOutPoint
        {
            get
            {
                GUIStyle style = new();
                style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/radio.png") as Texture2D;
                style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/radio on.png") as Texture2D;
                style.border = new RectOffset(4, 4, 12, 12);
                return style;
            }
        }

        public static GUIStyle Header
        {
            get
            {
                GUIStyle header = new();
                header.fontStyle = FontStyle.Bold;
                header.fontSize = 20;
                header.normal.textColor = Color.white;
                return header;
            }
        }

        public static GUIStyle Label
        {
            get
            {
                GUIStyle label = new();
                label.fontStyle = FontStyle.Bold;
                label.fontSize = 14;
                label.normal.textColor = Color.white;
                return label;
            }
        }

        public static GUIStyle LabelRight
        {
            get
            {
                GUIStyle label = new();
                label.fontStyle = FontStyle.Bold;
                label.fontSize = 14;
                label.normal.textColor = Color.white;
                label.alignment = TextAnchor.UpperRight;
                return label;
            }
        }
    }
}