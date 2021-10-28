using System;
using System.Collections.Generic;
using restaurantReviews.Repositories;
using week11practice.Models;

namespace restaurantReviews.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _reviewsRepository;

    public ReviewsService(ReviewsRepository reviewsRepository)
    {
      _reviewsRepository = reviewsRepository;
    }

    public List<Review> GetReviewsByRestaurantId(int id)
    {
      return _reviewsRepository.GetReviewsByRestaurantId(id);
    }

    public Review Create(Review newReview)
    {
      return _reviewsRepository.Create(newReview);
    }

    public Review Edit(Review editedReview)
    {
      Review foundReview = GetById(editedReview.Id);
      if (foundReview.CreatorId != editedReview.CreatorId)
      {
        throw new Exception("Unauthorized to edit");
      }
      foundReview.Body = editedReview.Body ?? foundReview.Body;
      foundReview.Title = editedReview.Title ?? foundReview.Title;
      foundReview.Rating = editedReview.Rating ?? foundReview.Rating;
      foundReview.Published = editedReview.Published ?? foundReview.Published;
      return _reviewsRepository.Edit(foundReview);
    }

    public void Delete(int id, string userId)
    {
      Review foundReview = GetById(id);
      if (foundReview.CreatorId != userId)
      {
        throw new Exception("Unauthorized to delete");
      }
      _reviewsRepository.Delete(id);
    }

    public List<RestaurantReview> GetReviewsByAccount(string userId)
    {
      return _reviewsRepository.GetReviewsByAccount(userId);
    }

    public Review GetById(int id)
    {
      Review foundReview = _reviewsRepository.GetById(id);
      if (foundReview == null)
      {
        throw new Exception("Unable to find that review");
      }
      return foundReview;
    }
  }
}