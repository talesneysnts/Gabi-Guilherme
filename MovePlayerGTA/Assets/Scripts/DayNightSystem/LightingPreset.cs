using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//criar um ScriptObj

[System.Serializable]
[CreateAssetMenu(fileName ="Lighting Preset", menuName ="Scriptables/Lighting Preset", order =1)]

public class LightingPreset : ScriptableObject
{
    //definir as predefinições pelo scriptObj as cores de ambiente, luz direcional e neblona (fog)...
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
