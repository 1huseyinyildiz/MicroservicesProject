﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T? Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSeccussful { get; set; }

        public List<string> Errors { get; set; }


        public static ResponseDto<T> Success(T? data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSeccussful = true };
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode, IsSeccussful = false };
        }

        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = errors,
                IsSeccussful = false
            };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = new List<string> { error},
                IsSeccussful = false
            };
        }
    }
}
