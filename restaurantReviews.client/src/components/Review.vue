<template>
  <div class="row my-3 border-dark border-bottom">
    <div class="col-md-2">
      <img :src="review.creator.picture" class="rounded-circle" alt="" />
      <small class="text-break">{{ review.creator.name }}</small>
    </div>
    <div class="col-md-10">
      <span>Rating: {{ review.rating }}</span>
      <span class="ms-5"
        >Dined On: {{ new Date(review.createdAt).toDateString() }}</span
      >
      <div
        class="float-end on-hover action"
        v-if="review.creatorId == account.id"
      >
        <i class="mdi mdi-delete text-danger" @click="deleteReview()"></i>
        <i
          class="mdi mdi-lead-pencil"
          data-bs-toggle="modal"
          :data-bs-target="'#edit-review-' + review.id"
        ></i>
      </div>
      <div class="d-flex py-2">
        {{ review.body }}
      </div>
    </div>
  </div>

  <!-- EDIT REVIEW MODAL -->
  <Modal :id="'edit-review-' + review.id">
    <template #modal-title>
      <h6>Edit your review!</h6>
    </template>
    <template #modal-body>
      <ReviewForm :review="review" />
    </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { Review } from "../models/Review"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { reviewsService } from "../services/ReviewsService"
export default {
  props: {
    review: {
      type: Review,
      default: () => { return new Review() }
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async deleteReview() {
        try {
          if (await Pop.confirm()) {
            await reviewsService.deleteReview(props.review.id)
            Pop.toast('Review Delorted!')
          }
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 3rem;
  width: 3rem;
}
</style>
