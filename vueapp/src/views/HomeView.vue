<template>
    <NavigationComponent></NavigationComponent>
    <AccountsComponent 
                       @newAccount="toggleAccountCreationModal" 
                       @newTransaction="toggleTransactionCreationModal"
                       @updateRole="updateAndToggle"
                       @setActiveTab="setActiveTab"
                       ></AccountsComponent>
    <AccountCreationComponent 
                        :accountCreationDialog="AccountCreationModal"
                        @createRole="this.updateRole = false"
                        @closeDialog="toggleAccountCreationModal" :updateRole="updateRole" @updateAccount="updateAccountObject"></AccountCreationComponent>
    <TransactionCreationComponent 
                        :AccountId="activeAccountId"
                        :TransactionCreationDialog="TransactionCreationModal"
                        @closeDialog="toggleTransactionCreationModal"
                        ></TransactionCreationComponent>
</template>

<script>
    import AccountsComponent from '../components/AccountsComponent';
    import AccountCreationComponent from '../components/AccountCreationComponent.vue';
    import TransactionCreationComponent from '../components/TransactionCreationComponent.vue';
    import NavigationComponent from '../components/NavigationComponent.vue';
    import { mapGetters, mapActions } from 'vuex';
    export default {
        beforeRouteEnter(to, from) {
            console.log(this)
        //    return 'false'
        },

        components: {
            AccountsComponent,
            AccountCreationComponent,
            TransactionCreationComponent,
            NavigationComponent
        },
        data() {
            return {
                accounts: [],
                AccountCreationModal: false,
                TransactionCreationModal: false,
                activeAccountId: null,
                updateRole: false
            }
        },
        props: {
            ...mapGetters([
                'getUser',
                'getAccounts'
            ])
        },
        methods: {
            ...mapActions([
                'addAccount',
                'updateAccount'
            ]),
            setActiveTab(activeTabId) {
                this.activeAccountId = activeTabId
            },
            toggleAccountCreationModal() {
                this.AccountCreationModal = !this.AccountCreationModal;
            },
            toggleTransactionCreationModal(evt, accountId) {
                this.activeAccountId = accountId
                this.TransactionCreationModal = !this.TransactionCreationModal;
            },
            updateAndToggle() {
                this.updateRole = true
                this.AccountCreationModal = !this.AccountCreationModal;
            },
            updateAccountObject(accountDto) {
                accountDto.accountId = this.activeAccountId
                console.log('will update: ', accountDto)

                this.updateAccount(accountDto)
                //    .then(() => {
                //        this.inputName = "";
                //        this.closeDialog();
                //    })
            }
        }
    }
</script>