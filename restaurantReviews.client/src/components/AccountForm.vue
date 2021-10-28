<template>
  <form @submit.prevent="editAccount()">
    <div class="form-group">
      <label for="name" class="sr-only"></label>
      <input
        type="text"
        name="name"
        class="form-control"
        placeholder="Name..."
        required
        v-model="editable.name"
      />
    </div>
    <div class="form-group">
      <label for="picture" class="sr-only"></label>
      <input
        type="url"
        name="picture"
        class="form-control"
        placeholder="picture..."
        required
        v-model="editable.picture"
      />
    </div>
    <button type="submit" class="btn btn-success">SAVE</button>
  </form>
</template>


<script>
import { ref } from "@vue/reactivity"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { accountService } from "../services/AccountService"
import { watchEffect } from "@vue/runtime-core"
import { AppState } from "../AppState"
import { Modal } from "bootstrap"
export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.account }
    })
    return {
      editable,
      async editAccount() {
        try {
          accountService.editAccount(editable.value)
          const modal = Modal.getOrCreateInstance(document.getElementById('edit-account'))
          modal.hide()
          Pop.toast('Account Edited!')
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
