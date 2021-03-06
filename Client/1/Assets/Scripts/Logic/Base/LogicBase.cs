﻿using Proto;
using Proto.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
    public abstract class LogicBase
    {
        /// <summary>
        /// 初始化客户端相关数据
        /// </summary>
        public abstract void InitData();
        /// <summary>
        /// 发送游戏协议
        /// </summary>
        /// <param name="cell"></param>
        public abstract void SendGamePacket(Cell_Base cell);
        /// <summary>
        /// 接受协议
        /// </summary>
        /// <param name="buffer"></param>
        public abstract bool OnRecivePacket(object _object, byte[] buffer);
        /// <summary>
        /// 接受单机协议
        /// </summary>
        /// <param name="_object"></param>
        /// <param name="buffer"></param>
        public abstract bool OnReciveConsole(object _object, byte[] buffer);
        /// <summary>
        /// 添加监听协议
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="cmd"></param>
        public abstract void AddPortocolListen(CallBack callback, ProtoCommand cmd);
        /// <summary>
        /// 本地循环
        /// </summary>
        public abstract void LogicFixedUpdate();
    }
}
