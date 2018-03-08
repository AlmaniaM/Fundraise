﻿using System;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class CreateFundraiser : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
    }
}