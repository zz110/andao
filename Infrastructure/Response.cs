namespace Infrastructure
{
    public class Response
    {
        public readonly static int SUCCESS_CODE = 200;

        public readonly static int ERROR_CODE = 500;

        /// <summary>
        /// 操作消息【当Status不为 200时，显示详细的错误信息】
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 操作状态码，200为正常
        /// </summary>
        public int Code { get; set; }

        public Response()
        {
            Code = 200;
            Message = "操作成功";
        }

    
    }


    /// <summary>
    /// WEBAPI通用返回泛型基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        public Response() : base()
        {

        }

        public Response(string errMsg)
        {
            Code = 500;
            Message = errMsg;
        }

        /// <summary>
        /// 回传的结果
        /// </summary>
        public T Result { get; set; }
    }
}
