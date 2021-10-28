using System;
using System.ComponentModel.DataAnnotations;
using restaurantReviews.Models;

namespace week11practice.Models
{
  public class Review
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Body { get; set; }
    public string Title { get; set; }

    // We can control the min/max of an int with range
    public Profile Creator { get; set; }
    [Range(1, 5)]
    // We can use ? if we want a field to be nullable
    [Required]
    public int? Rating { get; set; }
    public bool? Published { get; set; }
    public int RestaurantId { get; set; }
  }

  public class RestaurantReview : Review
  {
    public Restaurant Restaurant { get; set; }
  }
}