using MyBox;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Load Data", menuName = "ScriptableObject/Scene Load Data", order = 1)]
public class SceneLoadData : ScriptableObject
{
    public SceneReference SceneReference;
    public bool isAdditive;
}
