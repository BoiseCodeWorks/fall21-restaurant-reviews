using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using restaurantReviews.Interfaces;
using restaurantReviews.Models;
using week11practice.Models;

namespace restaurantReviews.Repositories
{
  public class RestaurantsRepository : IRepo<Restaurant, int>
  {
    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Restaurant Create(Restaurant newData)
    {
      throw new System.NotImplementedException();
    }

    public void Delete(int id)
    {
      throw new System.NotImplementedException();
    }

    public Restaurant Edit(Restaurant newData)
    {
      throw new System.NotImplementedException();
    }

    public List<Restaurant> GetAll()
    {
      string sql = "SELECT * FROM restaurants";
      return _db.Query<Restaurant>(sql).ToList();
    }

    public Restaurant GetById(int id)
    {
      string sql = @"
      SELECT
      r.*,
      a.*
      FROM restaurants r
      JOIN accounts a on a.id = r.creatorId
      WHERE r.id = @id;
      ";
      return _db.Query<Restaurant, Profile, Restaurant>(sql, (r, a) =>
     {
       r.Creator = a;
       return r;
     }, new { id }).FirstOrDefault();
    }
  }
}