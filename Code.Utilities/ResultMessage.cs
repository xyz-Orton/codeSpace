using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code.Utilities
{
    /// <summary>
    /// 结果消息，用于响应客户端的请求
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultMessage()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        public ResultMessage(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }


        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
