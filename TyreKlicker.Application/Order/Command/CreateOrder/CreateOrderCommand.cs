using MediatR;
using System;

namespace TyreKlicker.Application.Job.Command.CreateJob
{
    public class CreateOrderCommand : IRequest
    {
        public Guid ID { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }
    }
}