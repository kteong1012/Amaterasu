using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public static class  AOTReference
    {
        public static void Class()
        {
            GameObject go = new GameObject();
            go.AddComponent<NavMeshSurface>();
            NavMeshData data = new NavMeshData();
        }
    }
}
