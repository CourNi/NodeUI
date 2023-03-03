using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityTools.NodeUI
{
    [Serializable]
    public class ExecuteActions
    {
        public BaseAction OnExecute;
    }

    [Serializable]
    public class BaseAction : UnityEvent { }
}