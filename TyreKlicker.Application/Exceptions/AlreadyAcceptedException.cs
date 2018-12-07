using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.Application.Exceptions
{
    public class AlreadyAcceptedException : Exception
    {
        public AlreadyAcceptedException(string name, object key, Guid acceptedUserId)
            : base($"Entity \"{name}\" ({key}) is already accepted by user ({acceptedUserId}).")
        {
        }
    }
}
