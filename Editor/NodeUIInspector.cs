using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace UnityTools.NodeUI
{
    [CustomEditor(typeof(NodeUI))]
    public class NodeUIInspector : Editor
    {
        private void OnEnable()
        {
            
        }

        //public override void OnInspectorGUI()
        //{
        //    NodeUI myTarget = (NodeUI)target;
        //    serializedObject.Update();
        //    serializedObject.ApplyModifiedProperties();
        //}
    }
}