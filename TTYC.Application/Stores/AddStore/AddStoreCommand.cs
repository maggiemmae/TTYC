﻿using MediatR;

namespace TTYC.Application.Stores.AddStore
{
    public class AddStoreCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
