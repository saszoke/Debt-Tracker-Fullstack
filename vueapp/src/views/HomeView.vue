<template>
    <!--<v-card>
        <v-layout>
            <v-navigation-drawer class="bg-deep-purple"
                                 theme="dark"
                                 permanent>
                <v-list color="transparent">
                    <v-list-item prepend-icon="mdi-view-dashboard" title="Dashboard"></v-list-item>
                    <v-list-item prepend-icon="mdi-account-box" title="Account"></v-list-item>
                    <v-list-item prepend-icon="mdi-gavel" title="Admin"></v-list-item>
                </v-list>

                <template v-slot:append>
                    <div class="pa-2">
                        <v-btn block>
                            Logout
                        </v-btn>
                    </div>
                </template>
            </v-navigation-drawer>
            <v-main style="height: 400px"></v-main>
        </v-layout>
    </v-card>-->

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
    import { mapGetters, mapActions } from 'vuex';
    export default {
        components: {
            AccountsComponent,
            AccountCreationComponent,
            TransactionCreationComponent,
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