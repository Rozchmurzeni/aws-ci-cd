using System;
using LoanOfferer.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LoanOfferer.WebApi.Controllers
{
    [Route("api/loan-offer")]
    [ApiController]
    public class LoanOfferController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(CreateOfferRequest request) => throw new NotImplementedException();

        [HttpPut]
        public IActionResult Put(RequestLoanRequest request) => throw new NotImplementedException();
    }
}
