using UnityEngine.UIElements;

public interface IDraggable
{
    public void OnMouseDown(MouseDownEvent downEvent);

    public void OnMouseMove(MouseMoveEvent moveEvent);

    public void OnMouseUp(MouseUpEvent upEvent);
}
