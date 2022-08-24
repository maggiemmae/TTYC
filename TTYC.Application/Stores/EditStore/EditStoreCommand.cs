﻿using MediatR;

namespace TTYC.Application.Stores.EditStore
{
    public class EditStoreCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
