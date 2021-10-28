using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using restaurantReviews.Models;
using restaurantReviews.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using week11practice.Models;

namespace restaurantReviews.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly ReviewsService _reviewsService;
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService, ReviewsService reviewsService)
    {
      _accountService = accountService;
      _reviewsService = reviewsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("reviews")]

    public async Task<ActionResult<List<RestaurantReview>>> GetReviewsByAccount()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_reviewsService.GetReviewsByAccount(userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut]

    public async Task<ActionResult<Account>> Edit([FromBody] Account editedAccount)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return _accountService.Edit(editedAccount, userInfo.Email);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}