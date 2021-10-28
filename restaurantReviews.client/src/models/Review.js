
export class Review{
  constructor(data = {}){
    this.id = data.id
    this.creatorId = data.creatorId
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.body = data.body
    this.title = data.title
    this.creator = data.creator || {}
    this.rating = data.rating
    this.published = data.published
    this.restaurantId = data.restaurantId
    this.restaurant = data.restaurant || {}
  }
}
