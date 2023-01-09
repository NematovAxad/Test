
using SB.Common.Logics.SynonymProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ErrorException : Exception
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorException(string? message) : base(message)
        {
            Message = message;
        }

        public ErrorException(Enum errorEnum) : this(EnumSynonymProvider.Get(errorEnum))
        {
            Message = EnumSynonymProvider.Get(errorEnum);
        }

        public ErrorException(Enum errorEnum, string name) : this(name + " - " + EnumSynonymProvider.Get(errorEnum))
        {
            Message = name + " - " + EnumSynonymProvider.Get(errorEnum);
        }


    }
}
