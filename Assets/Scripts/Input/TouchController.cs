using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceMarine.Control
{
    /// <summary>
    /// Класс который организует работу с TouchWrapper и эмулирует кликами мышки в редакторе Touch.
    /// </summary>
    public class TouchController : MonoBehaviour
    {
        public delegate void TouchEventHandler(TouchWrapper touch, bool overUI);
        public static event TouchEventHandler TouchEndEvent = delegate { };
        public static event TouchEventHandler TouchStartEvent = delegate { };

        /// <summary>
        /// Хранятся обёртки для касания по айди этих касаний.
        /// </summary>
        private static Dictionary<int, TouchWrapper> touchWrappers = 
            new Dictionary<int, TouchWrapper>();

        /// <summary>
        /// Обновление всех касаний.
        /// </summary>
        private static void StaticUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Touch simTouch = GetTouchFromMouse();
                simTouch.phase = TouchPhase.Began;
                var wrapper = new TouchWrapper(simTouch);
                touchWrappers.Add(simTouch.fingerId, wrapper);
                TouchStartEvent?.Invoke(wrapper, EventSystem.current.IsPointerOverGameObject());
            }
            if (Input.GetMouseButton(0))
            {
                Touch simTouch = GetTouchFromMouse();
                simTouch.phase = TouchPhase.Moved;
                touchWrappers[0].UpdateTouch(simTouch);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Touch simTouch = GetTouchFromMouse();
                simTouch.phase = TouchPhase.Ended;
                touchWrappers[0].UpdateTouch(simTouch);
                TouchEndEvent?.Invoke(
                    touchWrappers[0], 
                    EventSystem.current.IsPointerOverGameObject()
                );
                touchWrappers.Clear();
            }
            lastMousePos = Input.mousePosition;
#endif
#if UNITY_ANDROID
            Touch touch;
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    var wrapper = new TouchWrapper(touch);
                    touchWrappers.Add(touch.fingerId, wrapper);
                    TouchStartEvent?.Invoke(
                        wrapper, 
                        EventSystem.current.IsPointerOverGameObject(touch.fingerId)
                    );
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    touchWrappers[touch.fingerId].UpdateTouch(touch);
                    TouchEndEvent?.Invoke(
                        touchWrappers[touch.fingerId], 
                        EventSystem.current.IsPointerOverGameObject(touch.fingerId)
                    );
                    touchWrappers.Remove(touch.fingerId);
                }
                else
                    touchWrappers[touch.fingerId].UpdateTouch(touch);
            }
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Используется для вычисления дельты перемещения мышки для более точно эмуляции касания.
        /// </summary>
        private static Vector3 lastMousePos;
        /// <summary>
        /// Создаёт фейковое касание из положения мышки.
        /// </summary>
        /// <returns></returns>
        private static Touch GetTouchFromMouse()
        {
            Touch output = new Touch();
            output.fingerId = 0;
            output.deltaPosition = Input.mousePosition - lastMousePos;
            output.position = Input.mousePosition;
            return output;
        }
#endif
        private void Update()
        {
            StaticUpdate();
        }
    }
}