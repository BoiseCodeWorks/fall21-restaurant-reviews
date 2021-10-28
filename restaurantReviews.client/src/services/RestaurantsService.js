import { AppState } from "../AppState"
import { Restaurant } from "../models/Restaurant"
import { Review } from "../models/Review"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RestaurantsService{

  async getAll(){
    const res = await api.get('api/restaurants')
    logger.log('get all restaurants res', res)
    AppState.restaurants = res.data.map(r => new Restaurant(r))
  }

  async getById(restaurantId){
    AppState.restaurant = null
    const res = await api.get(`api/restaurants/${restaurantId}`)
    logger.log('get restaurant by id res', res)
    AppState.restaurant = new Restaurant(res.data)
  }

  async getReviewsByRestaurant(restaurantId){
    AppState.reviews = []
    const res = await api.get(`api/restaurants/${restaurantId}/reviews`)
    logger.log('reviews by restaurant res', res)
    AppState.reviews = res.data.map(r => new Review(r))
  }
}

export const restaurantsService = new RestaurantsService()
