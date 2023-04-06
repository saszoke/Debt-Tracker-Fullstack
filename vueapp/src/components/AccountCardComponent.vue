
<template>
    <v-card class="mx-auto"
            max-width="344">
        <v-img src="https://www.solidbackgrounds.com/images/2560x1440/2560x1440-light-coral-solid-color-background.jpg"
               height="200px"
               cover>
            <v-avatar image="https://www.w3schools.com/howto/img_avatar2.png" size="160" class="mt-5"></v-avatar>
            <v-btn icon="mdi-pencil" size="x-small" style="position: absolute" class="my-5 mx-8" @click="this.$emit('updateRole')"></v-btn>
        </v-img>

        <v-card-title>
            Debt: {{ getBalance(account.id) }} Ft
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
            }
        },
        methods: {
            ...mapActions([
                'updateTransactions'
            ])
        },

        created() {
            this.updateTransactions({ accountId: this.account.id })
        }
    }
</script>