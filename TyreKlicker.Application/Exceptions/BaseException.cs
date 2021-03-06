﻿using System;

namespace TyreKlicker.Application.Exceptions
{
    public class BaseException : Exception
    {
        public string Code { get; set; }

        public string ErrorScenario { get; set; }

        protected BaseException(string errorCode, string errorScenario)
        {
            Code = errorCode;
            ErrorScenario = errorScenario;
        }
    }
}