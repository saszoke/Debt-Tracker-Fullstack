<template>
        <div class="text-center rounded-t elevation-2" style="background-color: #ffccd4">
            <v-btn :disabled="activeTab == null"
                   variant="text"
                   @click="deleteAccount">
                Remove account
            </v-btn>
            <v-divider class="mx-4"
                       vertical></v-divider>
            <v-btn variant="text"
                   @click="createAccount">
                Create account
            </v-btn>
        </div>
        <v-tabs v-model="tab"
                bg-color="red-lighten-2">
            <v-tab v-for="account in accounts"
                   :key="account"
                   :value="account"
                   @click="setActiveTab(account.id)"
                   >
                {{ account.name }}
            </v-tab>
        </v-tabs>
        <v-window v-model="tab" class="rounded-b-xl">
            <v-window-item v-for="account in accounts"
                           :key="account"
                           :value="account"
                           style="background-color: #ffccd4"
                           >
                <AccountCardComponent class="my-5"
                                      :account="account"
                                      @newTransaction="this.$emit('newTransaction', $event, account.id)"
                                      @updateRole="this.$emit('updateRole')"></AccountCardComponent>
            </v-window-item>
        </v-window>
        <AccountRemovalComponent :toDelete="activeTab" :show="showConfirm" @close="showConfirm = !showConfirm" @agreed="activeTab = null"></AccountRemovalComponent>
</template>

<script>
    import { mapGetters, mapActions, mapMutations } from 'vuex';
    import AccountCardComponent from './AccountCardComponent.vue';
    import AccountRemovalComponent from './AccountRemovalComponent.vue';

    export default {
        components: {
            AccountCardComponent,
            AccountRemovalComponent
        },
        data: () => ({
            showConfirm: false,
            accounts: [],
            tab: null,
            activeTab: null
        }),

        computed: {
            ...mapGetters([
                'getAccounts'
            ]),
        },
        methods: {
            ...mapActions([
                'addAccount',
                'updateAccounts',
                'removeAccount'
            ]),
            ...mapMutations([
                'setActiveAccount'
            ]),
            createAccount() {
                this.$emit('newAccount');
            },
            deleteAccount() {
                this.showConfirm = true
            },
            setActiveTab(active) {
                this.activeTab = active
                this.$emit('setActiveTab', active)
                this.setActiveAccount(active)

            }
        },

        beforeMount() {
            this.updateAccounts();
            this.accounts = this.getAccounts;
        },

  }
</script>