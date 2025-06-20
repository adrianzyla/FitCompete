using FitCompete.Api.Controllers;
using FitCompete.Domain.Entities;
using FitCompete.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitCompete.Api.Controllers
{
    public class ChallengeCategoriesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChallengeCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.Repository<ChallengeCategory>().GetAllAsync();
            return Ok(categories);
        }
    }
}