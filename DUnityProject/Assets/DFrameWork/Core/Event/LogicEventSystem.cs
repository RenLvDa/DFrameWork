using DFrameWork.Event.Dispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFrameWork
{
    public class LogicEventSystem : Singleton<LogicEventSystem>
    {
        public EventController<uint> _eventController = new EventController<uint>();

        public void CleanUp()
        {
            _eventController.CleanUp();
        }

        #region 增加监听器

        public void AddEventListener(uint eventType, Action handler, object data = null)
        {
            _eventController.AddMsgListener(eventType, handler, data);
        }

        public void AddEventListener<T>(uint eventType, Action<T> handler, object data = null)
        {
            _eventController.AddMsgListener<T>(eventType, handler, data);
        }

        public void AddEventListener<T, U>(uint eventType, Action<T, U> handler, object data = null)
        {
            _eventController.AddMsgListener<T, U>(eventType, handler, data);
        }

        public void AddEventListener<T, U, V>(uint eventType, Action<T, U, V> handler, object data = null)
        {
            _eventController.AddMsgListener<T, U, V>(eventType, handler, data);
        }

        public void AddEventListener<T, U, V, W>(uint eventType, Action<T, U, V, W> handler, object data = null)
        {
            _eventController.AddMsgListener<T, U, V, W>(eventType, handler, data);
        }

        #endregion

        #region 移除监听器

        public void RemoveEventListener(uint eventType, Action handler)
        {
            _eventController.RemoveMsgListener(eventType, handler);
        }

        public void RemoveEventListener<T>(uint eventType, Action<T> handler)
        {
            _eventController.RemoveMsgListener<T>(eventType, handler);
        }

        public void RemoveEventListener<T, U>(uint eventType, Action<T, U> handler)
        {
            _eventController.RemoveMsgListener<T, U>(eventType, handler);
        }

        public void RemoveEventListener<T, U, V>(uint eventType, Action<T, U, V> handler)
        {
            _eventController.RemoveMsgListener<T, U, V>(eventType, handler);
        }

        public void RemoveEventListener<T, U, V, W>(uint eventType, Action<T, U, V, W> handler)
        {
            _eventController.RemoveMsgListener<T, U, V, W>(eventType, handler);
        }

        #endregion

        #region 触发事件

        public void TriggerEvent(uint eventType)
        {
            _eventController.DispatchMsg(eventType);
        }

        public void TriggerEvent<T>(uint eventType, T para1)
        {
            _eventController.DispatchMsg<T>(eventType, para1);
        }

        public void TriggerEvent<T, U>(uint eventType, T para1, U para2)
        {
            _eventController.DispatchMsg<T, U>(eventType, para1, para2);
        }

        public void TriggerEvent<T, U, V>(uint eventType, T para1, U para2, V para3)
        {
            _eventController.DispatchMsg<T, U, V>(eventType, para1, para2, para3);
        }

        public void TriggerEvent<T, U, V, W>(uint eventType, T para1, U para2, V para3, W para4)
        {
            _eventController.DispatchMsg<T, U, V, W>(eventType, para1, para2, para3, para4);
        }
        #endregion
    }
}
