using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DFrameWork.Event.Dispatcher
{
    public class EventController<KeyT>
    {
        public Dictionary<KeyT, List<KeyValuePair<Delegate, object>>> _eventRouter = new Dictionary<KeyT, List<KeyValuePair<Delegate, object>>>();

        #region 基础操作
        /// <summary>
        /// 增加监听器前的检查
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="listenerBeingAdded"></param>
        /// <param name="list"></param>
        public void OnListenerAdding(KeyT msgId, Delegate listenerBeingAdded, out List<KeyValuePair<Delegate, object>> list)
        {
            //字典中没有此消息ID 创建一个
            if(!_eventRouter.TryGetValue(msgId, out list))
            {
                list = new List<KeyValuePair<Delegate, object>>();
                _eventRouter.Add(msgId, list);
            }

            if (list.Count > 0)
            {
                if (list[0].Key.GetType() != listenerBeingAdded.GetType())
                {
                    throw new Exception(string.Format(
                        "添加了错误的event {0}. 当前event类型是 {1}, 添加的event类型是 {2}.",
                        msgId, list[0].Key.GetType().Name, listenerBeingAdded.GetType().Name));
                }
                
                foreach(var kv in list)
                {
                    if(kv.Key == listenerBeingAdded)
                    {
                        throw new Exception(string.Format(
                       "尝试添加重复的委托事件 {0}. 当前event id是 {0},尝试添加的event是 {1}.",
                       msgId, listenerBeingAdded.Target.GetType().Name));
                    }
                }
            }
        }

        /// <summary>
        /// 移除监听器前的检查
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="listenerBeingAdded"></param>
        /// <param name="list"></param>
        public bool OnListenerRemoving(KeyT msgId, Delegate listenerBeingAdded, out List<KeyValuePair<Delegate, object>> list)
        {
            if(!_eventRouter.TryGetValue(msgId, out list))
            {
                return false;
            }

            if(list.Count > 0 && list[0].Key.GetType() != listenerBeingAdded.GetType())
            {
                throw new Exception(string.Format(
                       "Try to add not correct event {0}. Current type is {1}, adding type is {2}.",
                       msgId, list[0].Key.GetType().Name, listenerBeingAdded.GetType().Name));
            }

            return true;
        }

        /// <summary>
        /// 移除监听器之后的处理
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="list"></param>
        public void OnListenerRemoved(KeyT msgId, List<KeyValuePair<Delegate, object>> list)
        {
            if(list == null)
            {
                //删掉事件
                _eventRouter.Remove(msgId);
            }
        }

        /// <summary>
        /// 事件是否已存在
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <returns></returns>
        public bool ContainsMsg(KeyT msgId)
        {
            return _eventRouter.ContainsKey(msgId);
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void CleanUp()
        {
            _eventRouter.Clear();
        }

        #endregion

        #region 增加监听器

        /// <summary>
        /// 增加消息监听 无参数
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        public void AddMsgListener(KeyT msgId, Action handler, object data = null)
        {
            List<KeyValuePair<Delegate, object>> list;
            OnListenerAdding(msgId, handler, out list);
            list.Add(new KeyValuePair<Delegate, object>(handler, data));
        }

        /// <summary>
        /// 增加消息监听 1个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        public void AddMsgListener<T>(KeyT msgId, Action<T> handler, object data = null)
        {
            List<KeyValuePair<Delegate, object>> list;
            OnListenerAdding(msgId, handler, out list);
            list.Add(new KeyValuePair<Delegate, object>(handler, data));
        }

        /// <summary>
        /// 增加消息监听 2个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        public void AddMsgListener<T, U>(KeyT msgId, Action<T, U> handler, object data = null)
        {
            List<KeyValuePair<Delegate, object>> list;
            OnListenerAdding(msgId, handler, out list);
            list.Add(new KeyValuePair<Delegate, object>(handler, data));
        }

        /// <summary>
        /// 增加消息监听 3个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        public void AddMsgListener<T, U, V>(KeyT msgId, Action<T, U, V> handler, object data = null)
        {
            List<KeyValuePair<Delegate, object>> list;
            OnListenerAdding(msgId, handler, out list);
            list.Add(new KeyValuePair<Delegate, object>(handler, data));
        }

        /// <summary>
        /// 增加消息监听 4个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        /// <param name="data"></param>
        public void AddMsgListener<T, U, V, W>(KeyT msgId, Action<T, U, V, W> handler, object data = null)
        {
            List<KeyValuePair<Delegate, object>> list;
            OnListenerAdding(msgId, handler, out list);
            list.Add(new KeyValuePair<Delegate, object>(handler, data));
        }
        #endregion

        #region 移除监听器

        public bool RemoveHandlerInList(List<KeyValuePair<Delegate, object>> list, Delegate handler)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].Key == handler)
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除消息监听 无参数
        /// </summary>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        public void RemoveMsgListener(KeyT msgId, Action handler)
        {
            List<KeyValuePair<Delegate, object>> list;
            if(OnListenerRemoving(msgId, handler, out list))
            {
                RemoveHandlerInList(list, handler);
                OnListenerRemoved(msgId, list);
            }
        }

        /// <summary>
        /// 移除消息监听 1个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        public void RemoveMsgListener<T>(KeyT msgId, Action<T> handler)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (OnListenerRemoving(msgId, handler, out list))
            {
                RemoveHandlerInList(list, handler);
                OnListenerRemoved(msgId, list);
            }
        }

        /// <summary>
        /// 移除消息监听 2个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        public void RemoveMsgListener<T, U>(KeyT msgId, Action<T, U> handler)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (OnListenerRemoving(msgId, handler, out list))
            {
                RemoveHandlerInList(list, handler);
                OnListenerRemoved(msgId, list);
            }
        }

        /// <summary>
        /// 移除消息监听 3个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        public void RemoveMsgListener<T, U, V>(KeyT msgId, Action<T, U, V> handler)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (OnListenerRemoving(msgId, handler, out list))
            {
                RemoveHandlerInList(list, handler);
                OnListenerRemoved(msgId, list);
            }
        }

        /// <summary>
        /// 移除消息监听 4个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="handler"></param>
        public void RemoveMsgListener<T, U, V, W>(KeyT msgId, Action<T, U, V, W> handler)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (OnListenerRemoving(msgId, handler, out list))
            {
                RemoveHandlerInList(list, handler);
                OnListenerRemoved(msgId, list);
            }
        }
        #endregion

        #region 派发事件

        /// <summary>
        /// 分发消息事件 无参数
        /// </summary>
        /// <param name="msgId">消息ID</param>
        public void DispatchMsg(KeyT msgId)
        {
            List<KeyValuePair<Delegate, object>> list;
            if(!_eventRouter.TryGetValue(msgId, out list))
            {
                return;
            }

            for(int i = 0; i < list.Count; i++)
            {
                Action callback = list[i].Key as Action;

                if(callback == null)
                {
                    throw new Exception(string.Format("触发消息 {0} 错误: 参数类型不匹配", msgId));
                }

                try
                {
                    callback();
                }
                catch(Exception e)
                {
                    Debug.LogError("Error: [EventSystem]--[DispatchMsg]--" + e.ToString());
                }
            }
        }

        /// <summary>
        /// 分发消息事件 1个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="para1"></param>
        public void DispatchMsg<T>(KeyT msgId, T para1)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (!_eventRouter.TryGetValue(msgId, out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Action<T> callback = list[i].Key as Action<T>;

                if (callback == null)
                {
                    throw new Exception(string.Format("触发消息 {0} 错误: 参数类型不匹配", msgId));
                }

                try
                {
                    callback(para1);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error: [EventSystem]--[DispatchMsg]--" + e.ToString());
                }
            }
        }

        /// <summary>
        /// 分发消息事件 2个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="para1"></param>
        /// <param name="para2"></param>
        public void DispatchMsg<T, U>(KeyT msgId, T para1, U para2)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (!_eventRouter.TryGetValue(msgId, out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Action<T, U> callback = list[i].Key as Action<T, U>;

                if (callback == null)
                {
                    throw new Exception(string.Format("触发消息 {0} 错误: 参数类型不匹配", msgId));
                }

                try
                {
                    callback(para1, para2);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error: [EventSystem]--[DispatchMsg]--" + e.ToString());
                }
            }
        }

        /// <summary>
        /// 分发消息事件 3个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="para1"></param>
        /// <param name="para2"></param>
        /// <param name="para3"></param>
        public void DispatchMsg<T, U, V>(KeyT msgId, T para1, U para2, V para3)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (!_eventRouter.TryGetValue(msgId, out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Action<T, U, V> callback = list[i].Key as Action<T, U, V>;

                if (callback == null)
                {
                    throw new Exception(string.Format("触发消息 {0} 错误: 参数类型不匹配", msgId));
                }

                try
                {
                    callback(para1, para2, para3);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error: [EventSystem]--[DispatchMsg]--" + e.ToString());
                }
            }
        }

        /// <summary>
        /// 分发消息事件 4个参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="msgId">消息ID</param>
        /// <param name="para1"></param>
        /// <param name="para2"></param>
        /// <param name="para3"></param>
        /// <param name="para4"></param>
        public void DispatchMsg<T, U, V, W>(KeyT msgId, T para1, U para2, V para3, W para4)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (!_eventRouter.TryGetValue(msgId, out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Action<T, U, V, W> callback = list[i].Key as Action<T, U, V, W>;

                if (callback == null)
                {
                    throw new Exception(string.Format("触发消息 {0} 错误: 参数类型不匹配", msgId));
                }

                try
                {
                    callback(para1, para2, para3, para4);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error: [EventSystem]--[DispatchMsg]--" + e.ToString());
                }
            }
        }
        #endregion

        #region 使用params关键字不定长参数实现（弃用）
        /*
         * 消息系统不用这个的原因
         * 使用这种不定长参数虽然可以让参数数量不受限制，但是实际开发中没有办法严格规定参数顺序，具体调用时没有泛型<T,U,V,W>这样的约束，容易出错
         * 所以一般不用这种方法
         */
        public delegate void CallBack(params object[] objs);

        public void AddMsgListenerOld(KeyT msgId, CallBack handler, object data = null)
        {
            List<KeyValuePair<Delegate, object>> list;
            OnListenerAdding(msgId, handler, out list);
            list.Add(new KeyValuePair<Delegate, object>(handler, data));
        }


        public void RemoveMsgListenerOld(KeyT msgId, CallBack handler)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (OnListenerRemoving(msgId, handler, out list))
            {
                RemoveHandlerInList(list, handler);
                OnListenerRemoved(msgId, list);
            }
        }

        public void DispatchMsgOld(KeyT msgId, params object[] objs)
        {
            List<KeyValuePair<Delegate, object>> list;
            if (!_eventRouter.TryGetValue(msgId, out list))
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                CallBack callback = list[i].Key as CallBack;

                if (callback == null)
                {
                    throw new Exception(string.Format("触发消息 {0} 错误: 参数类型不匹配", msgId));
                }

                try
                {
                    callback(objs);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error: [EventSystem]--[DispatchMsg]--" + e.ToString());
                }
            }
        }
        #endregion
    }
}
