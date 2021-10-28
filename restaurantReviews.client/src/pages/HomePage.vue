<template>
  <div class="container-fluid">
    <div class="row">
      <Restaurant
        v-for="r in restaurants"
        :key="r.id"
        :restaurant="r"
        class="col-md-4 shadow rounded p-0 m-2"
      />
    </div>
  </div>
</template>

<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { restaurantsService } from '../services/RestaurantsService'
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
export default {
  name: 'Home',
  setup() {
    watchEffect(() => {
      try {
        restaurantsService.getAll()
      } catch (error) {
        Pop.toast(error.message, 'error')
      }
    })
    return {
      restaurants: computed(() => AppState.restaurants)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
