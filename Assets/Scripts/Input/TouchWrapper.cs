using UnityEngine;

namespace SpaceMarine.Control
{
    /// <summary>
    /// Обёртка вокруг Touch, которая добавляет ивенты.
    /// </summary>
    public class TouchWrapper
    {
        /// <summary>
        /// Квадрат изменения положения касания за один кадр, 
        /// при котором оно перестаёт быть Stationary.
        /// </summary>
        const float MAX_SQUARE_STANDALONE_DELTA = 25;
        /// <summary>
        /// Квадрат изменения положения касания, при котором оно перестаёт быть Stationary.
        /// </summary>
        const float MAX_SQUARE_STANDALONE_DISTANCE = 200;

        public readonly int fingerId;
        /// <summary>
        /// Начальная позиция касания.
        /// </summary>
        private Vector2 startPos;
        /// <summary>
        /// Является ли неподвижным.
        /// </summary>
        private bool isStationary = false;
        private Touch touchValue;

        public delegate void TouchEventHandler(Touch touch);
        public event TouchEventHandler TouchMoveEvent = delegate { };
        public event TouchEventHandler TouchEndEvent = delegate { };
        public event TouchEventHandler TouchStartEvent = delegate { };
        public event TouchEventHandler TouchStandaloneEvent = delegate { };

        public TouchWrapper(Touch touch)
        {
            fingerId = touch.fingerId;
            UpdateTouch(touch);
        }

        /// <summary>
        /// Обновляет информацию о касании.
        /// </summary>
        /// <param name="touch">Касание</param>
        public void UpdateTouch(Touch touch)
        {
            touchValue = touch;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TouchBegan(touch);
                    break;
                case TouchPhase.Stationary:
                    if (isStationary)
                    {
                        if (touch.deltaPosition.sqrMagnitude > MAX_SQUARE_STANDALONE_DISTANCE ||
                            (touch.position - startPos).sqrMagnitude > MAX_SQUARE_STANDALONE_DELTA)
                        {
                            isStationary = false;
                            TouchMoved(touch);
                        }
                        else
                            TouchStationary(touch);
                    }
                    else
                        TouchMoved(touch);
                    break;
                case TouchPhase.Moved:
                    if (isStationary)
                    {
                        if (touch.deltaPosition.sqrMagnitude > MAX_SQUARE_STANDALONE_DISTANCE ||
                            (touch.position - startPos).sqrMagnitude > MAX_SQUARE_STANDALONE_DELTA)
                        {
                            isStationary = false;
                            TouchMoved(touch);
                        }
                        else
                            TouchStationary(touch);
                    }
                    else
                        TouchMoved(touch);
                    break;
                case TouchPhase.Ended:
                    TouchEnded(touch);
                    break;
                case TouchPhase.Canceled:
                    TouchEnded(touch);
                    break;
            }
        }

        public void TouchBegan(Touch touch)
        {
            TouchStartEvent?.Invoke(touch);
            isStationary = true;
        }
        public void TouchStationary(Touch touch)
        {
            TouchStandaloneEvent?.Invoke(touch);
        }
        public void TouchMoved(Touch touch)
        {
            TouchMoveEvent?.Invoke(touch);
        }
        public void TouchEnded(Touch touch)
        {
            TouchEndEvent?.Invoke(touch);
            TouchEndEvent = null;
            TouchStartEvent = null;
            TouchMoveEvent = null;
            TouchMoveEvent = null;
        }
        public Touch GetTouch()
        {
            return touchValue;
        }
    }
}