import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }


  async editAccount(editedAccount){
    const res = await api.put('/account', editedAccount)
    logger.log('account edit res', res);
    AppState.account = res.data
  }
}

export const accountService = new AccountService()
