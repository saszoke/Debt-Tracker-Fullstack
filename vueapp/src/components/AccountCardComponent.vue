
<template>
    <v-card class="mx-auto rounded-b-xl"
            max-width="344">
        <v-img src="https://www.solidbackgrounds.com/images/2560x1440/2560x1440-light-coral-solid-color-background.jpg"
               height="200px"
               cover>
            <v-avatar :image="'https://localhost:7092/images/' + account.profilePicture" size="160" class="mt-5"></v-avatar>
            <v-btn icon="mdi-pencil" size="x-small" style="position: absolute" class="my-5 mx-8" @click="edit"></v-btn>
        </v-img>

        <v-card-title>
            Debt: {{ formatAmount(getBalance(account.id)) }}
        </v-card-title>

        <v-card-subtitle class="text-uppercase">
            {{ account.name }}
        </v-card-subtitle>

        <v-card-actions>
            <v-btn color="red-lighten-2"
                    variant="text" @click="show = !show">
                Details
            </v-btn>

            <v-spacer></v-spacer>

            <v-btn :icon="show ? 'mdi-chevron-up' : 'mdi-chevron-down'"
                    @click="show = !show"></v-btn>
        </v-card-actions>

        <v-expand-transition>
            <div v-show="show">
                <v-divider></v-divider>
                    <TransactionListComponent :accountId="account.id"></TransactionListComponent>
                <v-divider></v-divider>
                <v-card-actions>
                    <v-btn @click="this.$emit('newTransaction', $event, account.id)">Create transaction</v-btn>
                </v-card-actions>
            </div>
        </v-expand-transition>
    </v-card>
</template>

<script>
    import TransactionListComponent from "./TransactionListComponent.vue";
    import { mapGetters, mapActions } from 'vuex';
    export default {
        components: {
            TransactionListComponent
        },
        props: {
            account: Object
        },
        data() {
            return {
                show: false
            }
        },
        computed: {
            ...mapGetters([
                'getBalance'
            ]),
            accountDetails() {
                return this.account
            },
            
        },
        methods: {
            ...mapActions([
                'updateTransactions',
                'setUpdateId'
            ]),
            formatAmount(amount) {
                if (amount >= 10000 && amount <= 99999) {
                    return amount.toString().substr(0, 2) + " " + amount.toString().substring(2) + ' Ft'
                } else if (amount >= 100000 && amount <= 999999) {
                    return amount.toString().substr(0, 3) + " " + amount.toString().substring(3) + ' Ft'
                } else if (amount >= 1000000 && amount <= 9999999) {
                    return amount.toString().substr(0, 1) + " " + amount.toString().substr(1, 3) + " " + amount.toString().substring(4) + ' Ft'
                } else if (amount >= 10000000 && amount <= 99999999) {
                    return amount.toString().substr(0, 2) + " " + amount.toString().substr(2, 3) + " " + amount.toString().substring(5) + ' Ft'
                } else if (amount >= 100000000 && amount <= 999999999) {
                    return amount.toString().substr(0, 3) + " " + amount.toString().substr(3, 3) + " " + amount.toString().substring(6) + ' Ft'
                } else if (amount >= 1000000000 && amount <= 2147483647) {
                    return amount.toString().substr(0, 1) + " " + amount.toString().substr(1, 3) + " " + amount.toString().substr(3, 3) + " " + amount.toString().substring(7) + ' Ft'
                } else return amount + ' Ft'
            },
            edit() {
                console.log(this.account.id)
                this.setUpdateId(this.account.id)
                this.$emit('updateRole')
            }
        },

        created() {
            this.updateTransactions({ accountId: this.account.id })
        }
    }
</script>