import { createStore } from 'vuex'


const BASE_URL = 'https://localhost:7092'

const store = createStore({
    state: {
        user: {
            Username: null,
            Email: null,
            Token: null
        },
        accounts: [],
        transactions: [],
        updateObject: null,
        activeAccount: null,
        updateId: null
    },

    getters: {
        getUser(state) {
            return state.user
        },
        getAccounts(state) {
            return state.accounts
        },
        getAccount: (state) => (id) => {
            return state.accounts.filter(t => t.id == id)[0]
        },
        getTransactionsById: (state) => (id) => {
            return state.transactions.filter(t => t.id == id)[0]
        },
        getBalance: (state) => (id) => {
            return state.accounts.filter(t => t.id == id)[0].balance
        },
        getUpdateObject(state) {
            return state.updateObject
        },
        getActiveAccount(state) {
            return state.activeAccount
        },
        getUpdateId(state) {
            console.log(state.updateId)
            return state.updateId
        }
    },

    mutations: {
        setUser(state, payload) {
            state.user = payload
        },
        addAccount(state, payload) {
            payload.transactions = [];
            state.accounts.push(payload);
            console.log(payload)
            state.transactions.push(payload);
            console.log(payload)
        },
        updateAccount(state, payload) {
            const updateIndex = state.accounts.indexOf(state.accounts.filter(account => account.id == payload.accountId)[0])
            state.accounts[updateIndex].name = payload.Name
            state.accounts[updateIndex].profilePicture = payload.profilePicture
        },
        removeAccount(state, payload) {
            const removeIndex = state.accounts.indexOf(state.accounts.filter(account => account.id == payload)[0])
            state.accounts.splice(removeIndex,1)
        },
        addTransaction(state, payload) {

            state.transactions.filter(t => t.id == payload.accountId)[0]
                .transactions.push({ id: payload.id, amount: payload.amount, date: payload.transactionDate, isIncoming: payload.isIncoming })
        },
        updateTransaction(state, payload) {
            let transaction = state.transactions.filter(ts => ts.id == payload.accountId)[0]
                .transactions.filter(t => t.id == payload.id)[0]
            transaction.amount = payload.amount
            transaction.isIncoming = payload.isIncoming
            transaction.date = payload.transactionDate

        },
        removeTransaction(state, payload) {
            const index = state.transactions.filter(t => t.id == payload.accountId)[0].transactions.indexOf(state.transactions.filter(t => t.id == payload.accountId)[0]
                .transactions.filter(t => t.id == payload.id)[0])
            state.transactions.filter(t => t.id == payload.accountId)[0].transactions.splice(index,1)
            //state.transactions.filter(t => t.id == payload.accountId)[0]
            //    .transactions.filter(t => t.id == payload.id)[0]

        },
        setAccountBalance(state, payload) {
            state.accounts.filter(account => account.id == payload.id)[0].balance = payload.balance
        },
        setUpdateTransaction(state, payload) {
            state.updateObject = payload
        },
        setActiveAccount(state, payload) {
            state.activeAccount = payload
        },
        setUpdateId(state, payload) {
            state.updateId = payload
            console.log(state.updateId)
        }
    },

    actions: {
        setUser(context, payload) {
            context.commit('setUser', payload)
        },
        removeAccount(context, payload) {
            fetch(`${BASE_URL}/api/Accounts/${payload}`, {
                method: "DELETE",
                mode: "cors",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                redirect: "follow",
                referrerPolicy: "no-referrer",
            })
                .then(() => context.commit('removeAccount', payload))
        },
        addAccount(context, accountDto) {
            console.log('VUEX: ', accountDto)
            console.log('VUEX: ', accountDto.formData)
            fetch(`${BASE_URL}/api/Accounts`, {
                method: "POST", // *GET, POST, PUT, DELETE, etc.
                mode: "cors", // no-cors, *cors, same-origin
                cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
                //credentials: "same-origin", // include, *same-origin, omit
                headers: {
                    "Content-Type": "application/json",
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                },
                redirect: "follow", // manual, *follow, error
                referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
                body: JSON.stringify(accountDto)
            })
            .then(response => response.json())
                .then(account => {
                    console.log(account)
                    fetch(`${BASE_URL}/api/Accounts/uploadImage/${account.id}`, {
                        method: "POST",
                        body: accountDto.formData
                    })
                        .then(resp => resp.json())
                        .then(json => {
                            console.log(json)
                            account.profilePicture = json.imageName
                            context.commit('addAccount', account)
                        })
                })
        },
        updateAccount(context, payload) {
            fetch(`${BASE_URL}/api/Accounts/${payload.accountId}`, {
                method: "PUT", // *GET, POST, PUT, DELETE, etc.
                mode: "cors", // no-cors, *cors, same-origin
                cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
                //credentials: "same-origin", // include, *same-origin, omit
                headers: {
                    "Content-Type": "application/json",
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                },
                redirect: "follow", // manual, *follow, error
                referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
                body: JSON.stringify(payload)
            })
                .then(() => {
                    fetch(`${BASE_URL}/api/Accounts/uploadImage/${payload.accountId}`, {
                        method: "POST",
                        body: payload.formData
                    })
                        .then(resp => resp.json())
                        .then(json => {
                            payload.profilePicture = json.imageName
                            console.log(payload)
                            context.commit('updateAccount', payload)


                            //context.commit('updateAccount', payload)

                        })
                })
        },
        updateAccounts(context) {
            fetch(`${BASE_URL}/api/Accounts`)
                .then(response => {
                    return response.json()
                })
                .then(json => {
                    Array.from(json).forEach(account => {
                        context.commit('addAccount', account);
                    })
                })
        },
        updateTransactions(context, payload) {
            fetch(`${BASE_URL}/api/Transactions/Accounts/${payload.accountId}`)
                .then(response => response.json())
                .then(json => Array.from(json).forEach(transaction => {
                    context.commit('addTransaction', transaction)
                }));
            fetch(`${BASE_URL}/api/Accounts/${payload.accountId}/Balance`)
                .then(response => response.json())
                .then(json => context.commit('setAccountBalance', json))

        },
        addTransaction(context, payload) {
            fetch(`${BASE_URL}/api/Transactions`, {
                method: "POST",
                mode: "cors",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json",
                },
                redirect: "follow",
                referrerPolicy: "no-referrer",
                body: JSON.stringify(payload)
            })
                .then(response => response.json())
                .then(json => {
                    context.commit('addTransaction', json)
                })
                .then(() => {
                    fetch(`${BASE_URL}/api/Accounts/${payload.accountId}/Balance`)
                        .then(response => response.json())
                        .then(json => context.commit('setAccountBalance', json))
                })
        },
        updateTransaction(context, payload) {
            fetch(`${BASE_URL}/api/Transactions/${payload.TransactionId}`, {
                method: 'PUT',
                mode: 'cors',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                redirect: 'follow',
                referrerPolicy: 'no-referrer',
                body: JSON.stringify(payload)
            })
                .then(resp => resp.json())
                .then(json => {
                    context.commit('updateTransaction', json)
                })
                .then(() => {
                    fetch(`${BASE_URL}/api/Accounts/${payload.accountId}/Balance`)
                        .then(response => response.json())
                        .then(json => context.commit('setAccountBalance', json))
                })
        },
        removeTransaction(context, payload) {
            fetch(`${BASE_URL}/api/Transactions/${payload.id}`, {
                method: 'DELETE',
                mode: 'cors',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                redirect: 'follow',
                referrerPolicy: 'no-referrer',
                body: JSON.stringify(payload)
            })
                .then(resp => {
                    if (resp.ok) {
                        context.commit('removeTransaction', payload)
                        fetch(`${BASE_URL}/api/Accounts/${payload.accountId}/Balance`)
                            .then(response => response.json())
                            .then(json => context.commit('setAccountBalance', json))
                    }
                })

        },
        setUpdateId(context, payload) {
            context.commit('setUpdateId', payload)
        }
    }
})

export default store