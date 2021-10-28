<template>
  <div class="card m-2" style="width: 18rem">
    <img :src="restaurant.picture" class="card-img-top" alt="..." />
    <div class="card-body">
      <h6>{{ restaurant.name }}</h6>
      <small>Location: {{ restaurant.location }}</small>
    </div>
    <div class="card-footer">
      <router-link
        class="btn btn-info"
        :to="{ name: 'RestaurantDetails', params: { id: restaurant.id } }"
        >Details</router-link
      >
    </div>
  </div>
</template>


<script>
import { Restaurant } from "../models/Restaurant"
export default {
  props: {
    restaurant: {
      type: Restaurant,
      default: () => { return new Restaurant() }
    }
  },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped>
</style>
