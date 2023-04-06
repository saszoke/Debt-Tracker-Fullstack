<template>
        <div class="my-2"></div>
        <ConfirmDialogComponent :toDelete="toDelete" :show="confirm" @close="this.confirm = false"></ConfirmDialogComponent>
        <v-list class="text-center" style="max-height:200px">
            <v-list-subheader class="text-center">List of transactions</v-list-subheader>

            <v-list-item v-for="(transaction, i) in transactions"
                         :key="i"
                         :value="transaction.date"
                         :active-color="transaction.isIncoming ? '#b0d0b0' : '#ffcece'"
                         rounded="lg"
                         class="ma-2" variant="elevated">
                <template v-slot:prepend>
                    <v-tooltip :text="transaction.isIncoming ? 'Incoming transaction' : 'Outgoing transaction' " location="start">
                        <template v-slot:activator="{ props }">
                            <v-icon v-bind="props" :style="transaction.isIncoming ? inColor : outColor " :icon="transaction.isIncoming ? 'mdi-cash-check' : 'mdi-cash-remove'"></v-icon>
                        </template>
                    </v-tooltip>
                    

                    <v-menu location="top">
                        <template v-slot:activator="{ props }">
                            <v-icon icon="mdi-dots-vertical" v-bind="props"></v-icon>
                        </template>

                        <v-list>
                            <v-list-item>
                                <v-btn variant="text" @click="editTransaction(transaction)">Edit</v-btn>
                            </v-list-item>
                            <v-list-item>
                                <v-btn variant="text" @click="deleteTransaction(transaction)">Delete</v-btn>
                            </v-list-item>

                        </v-list>
                    </v-menu>
                    <!--<v-btn icon="mdi-dots-vertical" class="plain"> </v-btn>-->
                    <!--<v-icon :style="transaction.isIncoming ? inColor : outColor " :icon="transaction.isIncoming ? 'mdi-cash-check' : 'mdi-cash-remove'"></v-icon>
    <v-icon :style="transaction.isIncoming ? inColor : outColor " :icon="transaction.isIncoming ? 'mdi-cash-check' : 'mdi-cash-remove'"></v-icon>-->

                </template>

                <v-list-item-title v-text="formatAmount(transaction.amount)"></v-list-item-title>
                <v-list-item-subtitle v-text="transaction.date.toString().substr(0,10)"></v-list-item-subtitle>
            </v-list-item>
        </v-list>
        <TransactionUpdateComponent :show="showUpdateModal"  @closeDialog="this.showUpdateModal = false"></TransactionUpdateComponent>
</template>

<script>
    import { defineComponent } from "vue";
    import { mapGetters, mapMutations } from 'vuex';
    import ConfirmDialogComponent from "./ConfirmDialogComponent.vue";
    import TransactionUpdateComponent from "./TransactionUpdateComponent.vue";

    export default defineComponent({
        components: {
            TransactionUpdateComponent,
            ConfirmDialogComponent
        },
        data() {
            return {
                transactions: [],
                showUpdateModal: false,
                outColor: {
                    color: 'red'
                },
                inColor: {
                    color: 'green'
                },
                confirm: false,
                toDelete: null
            }
        },
        props: {
            accountId: Number
        },
        created() {
            this.transactions = this.getTransactionsById(this.accountId).transactions
        },
        computed: {
            ...mapGetters([
                'getTransactionsById'
            ])
            
        },
        methods: {
            ...mapMutations([
                'setUpdateTransaction'
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
            editTransaction(transaction) {
                this.setUpdateTransaction(transaction)
                this.showUpdateModal = true
            },
            deleteTransaction(transaction) {
                console.log(transaction)
                this.confirm = !this.confirm
                this.toDelete = transaction
            }
        }
    });

</script>

<style>


</style>