using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityTools.NodeUI
{
    public class NodeList : MonoBehaviour
    {
        public static readonly Dictionary<string, Type> Nodes = new()
        {
            { "Component/Execute", typeof(ExecuteNode) },
            { "Component/Base", typeof(ComponentNode) },
            { "Component/Image", typeof(ImageNode) },
            { "Component/Text", typeof(TextNode) },
            { "Event/OnClick", typeof(OnClick) },
            { "Event/OnEnter", typeof(OnEnter) },
            { "Event/OnExit", typeof(OnExit) },
            { "Time/Delay", typeof(ActionNodeDelay) },
            { "Action/Color/Fade", typeof(ActionNodeFade) },
            { "Action/Transform/Set", typeof(ActionNodeTransform) },
            { "Action/Sprite/Set", typeof(ActionNodeSprite) }
        };

        public static string GetCategory(Type type) => Regex.Match(Nodes.FirstOrDefault(i => i.Value == type).Key, "[A-z]+").Value;
        public static string GetCategoryByKey(string key) => Regex.Match(key, "[A-z]+").Value;
    }
}