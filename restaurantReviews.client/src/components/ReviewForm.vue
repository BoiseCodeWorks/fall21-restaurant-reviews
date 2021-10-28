<template>
  <form @submit.prevent="handleSubmit()">
    <!-- review body -->
    <div class="form-group">
      <label for="body" class="sr-only"></label>
      <textarea
        type="text"
        name="body"
        class="form-control"
        placeholder="Review here"
        required
        v-model="editable.body"
      />
    </div>
    <!-- rating -->
    <div class="form-group">
      <label for="rating" class="sr-only"></label>
      <input
        type="number"
        name="rating"
        class="form-control"
        min="1"
        max="5"
        placeholder="Rating"
        required
        v-model="editable.rating"
      />
    </div>
    <!-- published -->
    <div class="form-group">
      <label for="published">Publish </label>
      <input
        type="checkbox"
        class="ms-3 mt-3"
        name="published"
        v-model="editable.published"
      />
    </div>
    <button type="submit" class="btn btn-success mt-1">Submit</button>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { reviewsService } from '../services/ReviewsService'
import { useRoute } from "vue-router"
import { Modal } from "bootstrap"
import { Review } from "../models/Review"
import { watchEffect } from "@vue/runtime-core"
export default {
  props: {
    review: {
      type: Review,
      default: () => new Review()
    }
  },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.review }
    })
    const route = useRoute()
    return {
      editable,
      async handleSubmit() {
        try {
          if (editable.value.id) {
            await reviewsService.editReview(editable.value)
            const editModal = Modal.getInstance(document.getElementById(`edit-review-${props.review.id}`))
            editModal.hide()
            Pop.toast('Review Editored!')
            return
          }
          editable.value.restaurantId = route.params.id
          await reviewsService.createReview(editable.value)
          const modal = Modal.getInstance(document.getElementById('create-review'))
          modal.hide()
          editable.value = {}
          Pop.toast('Review Submitted!', 'success')
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
</style>
