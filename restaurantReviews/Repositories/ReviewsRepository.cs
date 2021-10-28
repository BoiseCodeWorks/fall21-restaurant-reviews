using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using restaurantReviews.Interfaces;
using restaurantReviews.Models;
using week11practice.Models;

namespace restaurantReviews.Repositories
{
  public class ReviewsRepository : IRepo<Review, int>
  {
    private readonly IDbConnection _db;

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Review Create(Review newData)
    {
      string sql = @"
      INSERT INTO reviews(body, title, rating, published, restaurantId, creatorId)
      VALUES(@Body, @Title, @Rating, @Published, @RestaurantId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, newData);
      newData.Id = id;
      return newData;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM reviews WHERE id = @id LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected == 0)
      {
        throw new Exception("Review Delorte failed");
      }
    }

    public Review Edit(Review newData)
    {
      string sql = @"
      UPDATE reviews
      SET
      body = @Body,
      title = @Title,
      rating = @Rating,
      published = @Published
      WHERE id = @Id LIMIT 1;";
      var rowsAffected = _db.Execute(sql, newData);
      if (rowsAffected == 0)
      {
        throw new Exception("Update review failed");
      }
      return newData;
    }

    public List<Review> GetReviewsByRestaurantId(int id)
    {
      string sql = @"
      SELECT 
      r.*,
      a.*
      FROM reviews r
      JOIN accounts a on a.id = r.creatorId
      WHERE r.restaurantId = @id AND published = 1;
      ";
      return _db.Query<Review, Profile, Review>(sql, (r, a) =>
      {
        r.Creator = a;
        return r;
      }, new { id }).ToList();
    }

    public List<RestaurantReview> GetReviewsByAccount(string userId)
    {
      string sql = @"
      SELECT
      r.*,
      a.*,
      rt.*
      FROM reviews r
      JOIN accounts a on a.id = r.creatorId
      JOIN restaurants rt on rt.id = r.restaurantId
      WHERE r.creatorId = @userId;";
      return _db.Query<RestaurantReview, Profile, Restaurant, RestaurantReview>(sql, (r, a, rt) =>
      {
        r.Creator = a;
        r.Restaurant = rt;
        return r;
      }, new { userId }).ToList();
    }

    // DO NOT NEED THIS ROUTE - IT DOESN'T MAKE SENSE FOR THIS APPLICATION
    public List<Review> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public Review GetById(int id)
    {
      string sql = @"
      SELECT
      r.*,
      a.*,
      rt.*
      FROM reviews r 
      JOIN accounts a on a.id = r.creatorId
      JOIN restaurants rt on rt.id = r.restaurantId
      WHERE r.id = @id;
      ";
      return _db.Query<RestaurantReview, Profile, Restaurant, RestaurantReview>(sql, (r, a, rt) =>
      {
        r.Restaurant = rt;
        r.Creator = a;
        return r;
      }, new { id }).FirstOrDefault();
    }
  }
}