using System;
using System.Collections.Generic;
using restaurantReviews.Repositories;
using week11practice.Models;

namespace restaurantReviews.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _restaurantsRepository;

    public RestaurantsService(RestaurantsRepository restaurantsRepository)
    {
      _restaurantsRepository = restaurantsRepository;
    }

    public List<Restaurant> GetAll()
    {
      return _restaurantsRepository.GetAll();
    }

    public Restaurant GetById(int id)
    {
      Restaurant foundRestaurant = _restaurantsRepository.GetById(id);
      if (foundRestaurant == null)
      {
        throw new Exception("Unable to find that restaurant");
      }
      return foundRestaurant;
    }

    internal Restaurant Create(Restaurant newRestaurant)
    {
      throw new NotImplementedException();
    }

    internal object Edit(Restaurant editedRestaurant)
    {
      throw new NotImplementedException();
    }

    internal void Delete(int id1, string id2)
    {
      throw new NotImplementedException();
    }
  }
}