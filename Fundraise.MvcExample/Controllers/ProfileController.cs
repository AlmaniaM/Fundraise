﻿using Fundraise.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Fundraise.MvcExample.Models;
using Fundraise.Core.Entities;

namespace Fundraise.MvcExample.Controllers
{
    public class ProfileController : Controller
    {


        private ICampaignRepository _campaignRepository;
        private IFundraiserRepository _fundraiserRepository;
        private IDonationRepository _donationRepository;

        public ProfileController(CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository, IDonationRepository donationRepository)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
            _donationRepository = donationRepository;
        }

        // GET: Profile
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            string userId = User.Identity.GetUserId();
            var model = new ProfileViewModel();
            var donations =_donationRepository.GetByDonor(userId).ToList();
            model.Donations = AutoMapper.Mapper.Map<List<Donation>, List<DonationViewModel>>(donations);

            // a better way? this is not efficient
            foreach (var donation in model.Donations)
            {
                var fundraiser = _fundraiserRepository.FindById(donation.FundraiserId);
                if (fundraiser != null)
                {
                    donation.FundraiserName = fundraiser.Name;
                }
                var campaign = _campaignRepository.FindById(donation.CampaignId);
                if (campaign != null)
                {
                    donation.CampaignName = campaign.Name;
                }
            }

            model.Fundraisers = new List<FundraiserViewModel>();

            return View(model);
            
        }
    }
}