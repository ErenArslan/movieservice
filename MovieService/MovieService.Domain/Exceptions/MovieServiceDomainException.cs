using System;
using System.Collections.Generic;
using System.Text;

namespace MovieService.Domain.Exceptions
{
   public class MovieServiceDomainException
   : Exception
    {
        public MovieServiceDomainException()
        { }

        public MovieServiceDomainException(string message)
            : base(message)
        { }

        public MovieServiceDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
