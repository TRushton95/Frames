namespace Frames.EventSystem
{
    public static class EventTypes
    {
        public static class Frames
        {
            public static EventType ElementHover = EventType.GetEventType("FRAMES_ELEMENT-HOVER");
            public static EventType ElementHoverLeave = EventType.GetEventType("FRAMES_ELEMENT-HOVER-LEAVE");
            public static EventType ElementClick = EventType.GetEventType("FRAMES_ELEMENT-CLICK");
            public static EventType ElementMouseWheelScrollUp = EventType.GetEventType("FRAMES_ELEMENT_MOUSE_WHEEL_SCROLL_UP");
            public static EventType ElementMouseWheelScrollDown = EventType.GetEventType("FRAMES_ELEMENT_MOUSE_WHEEL_SCROLL_DOWN");
        }
    }
}
