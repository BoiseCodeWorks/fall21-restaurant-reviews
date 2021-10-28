import { AppState } from "../AppState"
import { Review } from "../models/Review"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ReviewsService{

  async createReview(reviewData){
    const res = await api.post('api/reviews', reviewData)
    logger.log('create review res', res)
    AppState.reviews = [new Review(res.data), ...AppState.reviews]
  }

  async deleteReview(reviewId){
    const res = await api.delete(`api/reviews/${reviewId}`)
    logger.log('delete review res', res.data)
    AppState.reviews = AppState.reviews.filter(r => r.id !== reviewId)
  }

  async editReview(reviewData){
    const res = await api.put(`api/reviews/${reviewData.id}`, reviewData)
    logger.log('edit review res', res)
    let reviewIndex = AppState.reviews.findIndex(r => r.id == reviewData.id)
    AppState.reviews.splice(reviewIndex, 1, new Review(res.data))
  }

  async getReviewsByAccount(){
    AppState.reviews = []
    const res = await api.get('account/reviews')
    logger.log('account reviews res', res.data)
    AppState.reviews = res.data.map(r => new Review(r))

  }
}

export const reviewsService = new ReviewsService()
