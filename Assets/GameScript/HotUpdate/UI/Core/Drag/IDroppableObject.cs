using UnityEngine.EventSystems;

namespace Game
{
    public interface IDroppableObject : IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
    }
}
