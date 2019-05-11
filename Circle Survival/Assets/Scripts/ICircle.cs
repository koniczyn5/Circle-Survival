using UnityEngine.EventSystems;

public interface ICircle: IPointerDownHandler
{
    float InitialTime { get; }
    float Timer { get; }
    
    
}
