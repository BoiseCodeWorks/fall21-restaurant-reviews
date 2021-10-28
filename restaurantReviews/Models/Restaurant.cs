using System;
using restaurantReviews.Models;

namespace week11practice.Models
{
  public class Restaurant
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public Profile Creator { get; set; }

  }
}