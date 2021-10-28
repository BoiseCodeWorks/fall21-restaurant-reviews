<template>
  <!-- alternatively we could use a v-if here and have the same result as using the elvis operator - ? -->
  <div class="container-fluid" v-if="restaurant">
    <!-- COVER IMAGE ROW -->
    <div class="row img-row"></div>
    <div class="row justify-content-center my-3">
      <div class="col-md-8 border rounded shadow">
        <div class="d-flex justify-content-between">
          <h2>{{ restaurant.name }}</h2>
          <button
            class="btn btn-success"
            data-bs-toggle="modal"
            data-bs-target="#create-review"
          >
            Leave Review
          </button>
        </div>
        <div v-if="reviews.length > 0">
          <span>Reviews: {{ reviews.length }}</span>
          <span class="ms-5" v-if="average">Average: {{ average }} </span>
        </div>
        <span v-else>No reviews yet.....</span>
        <!-- REVIEW SECTION -->
        <div class="row pt-4">
          <h6>What Customers Have To Say....</h6>
          <div class="col" v-if="reviews.length > 0">
            <Review v-for="r in reviews" :key="r.id" :review="r" />
          </div>
          <div class="col" v-else>
            <h6>No Reviews yet</h6>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- CREATE REVIEW MODAL -->

  <Modal id="create-review">
    <template #modal-title>
      <h6>Leave a Review!</h6>
    </template>
    <template #modal-body>
      <ReviewForm />
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { restaurantsService } from "../services/RestaurantsService"
import { useRoute } from "vue-router"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      try {
        restaurantsService.getById(route.params.id)
        restaurantsService.getReviewsByRestaurant(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      route,
      restaurant: computed(() => AppState.restaurant),
      reviews: computed(() => AppState.reviews),
      average: computed(() => {
        let total = 0
        AppState.reviews.forEach(r => { total += r.rating })
        return Math.ceil(total / AppState.reviews.length)
      }),
      rImage: computed(() => `url(${AppState.restaurant?.picture})`)
    }
  }
}
</script>


<style scoped>
.img-row {
  background-image: v-bind(rImage);
  min-height: 40vh;
  background-size: cover;
  background-repeat: no-repeat;
}
</style>
