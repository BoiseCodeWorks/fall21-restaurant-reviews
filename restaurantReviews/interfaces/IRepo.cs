using System.Collections.Generic;

namespace restaurantReviews.Interfaces
{
  public interface IRepo<T, idType>
  {
    List<T> GetAll();
    T GetById(idType id);
    T Create(T newData);
    T Edit(T newData);
    void Delete(idType id);
  }
}