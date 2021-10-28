<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-9 d-flex">
        <img :src="account.picture" class="rounded mt-2" alt="" />
        <div class="flex-row ms-5">
          <h4>Name: {{ account.name }}</h4>
          <h4>Email: {{ account.email }}</h4>
          <button
            class="btn btn-success"
            data-bs-toggle="modal"
            data-bs-target="#edit-account"
          >
            Edit Account
          </button>
        </div>
      </div>
    </div>
    <div class="row mt-5">
      <h3>Your Reviews</h3>
      <Review v-for="r in reviews" :key="r.id" :review="r" />
    </div>
  </div>

  <!-- EDIT ACCOUNT MODAL -->
  <Modal id="edit-account">
    <template #modal-title>
      <h6>Edit Account!</h6>
    </template>
    <template #modal-body>
      <AccountForm />
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import { reviewsService } from "../services/ReviewsService"
export default {
  name: 'Account',
  setup() {
    onMounted(() => {
      try {
        reviewsService.getReviewsByAccount()
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      account: computed(() => AppState.account),
      reviews: computed(() => AppState.reviews)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
