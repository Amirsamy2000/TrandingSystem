using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.Shared.Response
{
    public class Response<T>
    {

        public T Data { get; set; }
        public T? Data2 { get; set; }
        public string message { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode status { get; set; }
        public bool Success { get; set; }

        public Response() { }
        /// <summary>
        /// use if every thing is okay 
        /// </summary>
        /// <param name="Data">your data which you want to return</param>
        public Response(T Data)
        {
            this.Data = Data;
            this.message = "every thing is okay";
            this.status = HttpStatusCode.OK;
            Success = true;

        }

        /// <summary>
        /// use if every thing is okay but by your custom message
        /// </summary>
        /// <param name="SuccessMessage"> your custom success message</param>
        /// <param name="Data">your data which you want to return</param>
        public Response(string SuccessMessage, T Data)
        {
            this.Data = Data;
            this.message = SuccessMessage;
            this.status = HttpStatusCode.OK;
            Success = true;


        }

        /// <summary>
        /// use if every thing is okay but by your custom message
        /// </summary>
        /// <param name="SuccessMessage"> your custom success message</param>
        /// <param name="Data">your data which you want to return</param>
        /// <param name="Data2">your optional additional data which you want to return</param>
        public Response(string SuccessMessage, T Data, T? Data2)
        {
            this.Data = Data;
            this.Data2 = Data2;
            this.message = SuccessMessage;
            this.status = HttpStatusCode.OK;
            Success = true;

        }

        /// <summary>
        /// use if there is an error but by your custom error message
        /// </summary>
        /// <param name="Data">your data which you want to return</param>
        /// <param name="Error_message">your custom Error message</param>
        public Response(T Data, string Error_message, HttpStatusCode status = HttpStatusCode.ExpectationFailed)
        {
            this.Data = Data;
            this.message = "Failed  : " + Error_message;
            this.ErrorMessage = Error_message;
            this.status = status;
            Success = false;


        }

        /// <summary>
        /// use if there is an error but by your custom error message and the Exception message
        /// </summary>
        /// <param name="Data">your data which you want to return</param>
        /// <param name="Error_message">your custom error message</param>
        /// <param name="Exception_Msg">the exception message</param>
        public Response(T Data, string Error_message, string Exception_Msg, HttpStatusCode status = HttpStatusCode.ExpectationFailed)//
        {
            this.Data = Data;
            this.message = Error_message;
            this.ErrorMessage = Exception_Msg;
            this.status = HttpStatusCode.ExpectationFailed;
            Success = false;


        }

        /// <summary>
        /// use if there is an error but by your custom error message
        /// </summary>
        /// <param name="Error_message">your custom error message</param>
        public Response(string Error_message, HttpStatusCode status = HttpStatusCode.ExpectationFailed)//use if there is an error 
        {
            this.message = "Failed  : " + Error_message;
            this.ErrorMessage = Error_message;
            this.status = HttpStatusCode.ExpectationFailed;
            Success = false;

        }
        /// <summary>
        /// use if there is an error but by just your custom error message and the Exception message
        /// </summary>
        /// <param name="Error_message">your custom error message</param>
        /// <param name="Exception_Msg">the exception message</param>
        public Response(string Error_message, string Exception_Msg, HttpStatusCode status = HttpStatusCode.ExpectationFailed)//use if there is an error 
        {
            this.message = Error_message;
            this.ErrorMessage = Exception_Msg;
            this.status = status;
            Success = false;

        }

    }
}
