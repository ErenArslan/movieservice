﻿using System.Collections.Generic;
using System.Linq;

namespace MovieService.Application.Dtos.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }

        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public int Total { get; set; }
        public T Data { get; set; }
    }
}
