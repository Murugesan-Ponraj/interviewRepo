using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.Common.Model
{

    public class ApiResponse<T> where T :  new()
    {
        public bool Succeeded => Exception is null;
        public Exception? Exception { get; set; }
        public dynamic Value { get; set; }

        public static ApiResponse<T> Fail(Exception e)
        {
            return new ApiResponse<T>
            {
                Exception = e
            };
        }

        public static ApiResponse<T> Succeed(T value)
        {
            return new ApiResponse<T>
            {
                Value = value
            };
        }

        public static ApiResponse<T> Succeed(IEnumerable<T>  value)
        {
            return new ApiResponse<T>
            {
                Value = value
            };
        } 
    
    } 

}
