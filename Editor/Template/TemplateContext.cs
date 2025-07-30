using System;
using UnityEngine;

namespace State_Machine_Creator.Editor.Template
{
    [Serializable]
    public class TemplateContext
    {
        [field: SerializeField] public string SomeField { get; set; }
    }
}