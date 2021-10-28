using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restaurantReviews.Models;
using restaurantReviews.Services;
using week11practice.Models;

namespace restaurantReviews.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReviewsController : ControllerBase
  {
    private readonly ReviewsService _reviewsService;

    public ReviewsController(ReviewsService reviewsService)
    {
      _reviewsService = reviewsService;
    }

    [HttpGet("{id}")]

    public ActionResult<Review> GetById(int id)
    {
      try
      {
        return Ok(_reviewsService.GetById(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Review>> Create([FromBody] Review newReview)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // NEVER TRUST THE CLIENT
        newReview.CreatorId = userInfo.Id;
        Review createdReview = _reviewsService.Create(newReview);
        createdReview.Creator = userInfo;
        return Ok(createdReview);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]

    public async Task<ActionResult<Review>> Edit([FromBody] Review editedReview, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedReview.CreatorId = userInfo.Id;
        editedReview.Id = id;
        return Ok(_reviewsService.Edit(editedReview));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]

    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _reviewsService.Delete(id, userInfo.Id);
        return Ok("Review Was Deleted!");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}