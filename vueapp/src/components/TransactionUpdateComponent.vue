<template>
    <div class="text-center">
        <v-dialog v-model="showModal"
                  activator="parent"
                  width="auto"
                  transition="dialog-bottom-transition">
            <v-card style="overflow:visible">
                <v-card-title>
                    <span class="text-h5">Update transaction</span>
                </v-card-title>
                <v-card-text>
                    <v-container>
                        <v-sheet width="300" class="mx-auto">
                            <v-form @submit.prevent v-model="form">
                                <v-text-field v-model="amount"
                                              :rules="amountRules"
                                              label="Amount (HUF)"></v-text-field>
                                <v-switch v-model="incoming"
                                          hide-details
                                          inset
                                          :color="incoming ? '#4b934b' : '#ffcece'"
                                          label="Incoming Transaction"></v-switch>
                                <VueDatePicker v-model="date" :enable-time-picker="false"></VueDatePicker>
                                <!--<v-btn :disabled="!form" type="submit" @click="createHandler" block class="mt-2">Submit</v-btn>-->
                            </v-form>
                        </v-sheet>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue-darken-1"
                           variant="text"
                           @click="this.$emit('closeDialog')">
                        Close
                    </v-btn>
                    <v-btn color="blue-darken-1"
                           variant="text"
                           @click="updateHandler" :disabled="getUpdateObject.amount < 1000">
                        Update
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>

</template>
<script>
    import VueDatePicker from '@vuepic/vue-datepicker';
    import '@vuepic/vue-datepicker/dist/main.css';
    import { mapGetters, mapActions } from 'vuex';

    export default {
        components: {
            VueDatePicker
        },
        props: {
            TransactionCreationDialog: Boolean,
            AccountId: Number,
            show: Boolean
        },

        data: () => ({
            activeAccountId: null,
            form: true,
            amount: 1001,
            incoming: true,
            date: new Date().toString(),
            amountRules: [
                function(value) {
                        if (value && value >= 1000 && value < 2147483647) {
                            return true
                    }
                    return 'Please enter a valid amount (minimum amount: 1000)'
                },
            ],
            shallNotPass: true
        }),
        computed: {
            ...mapGetters([
                'getUpdateObject',
                'getActiveAccount'
            ]),
            showModal() {
                return this.show
            }
        },
        methods: {
            ...mapActions([
                'addTransaction',
                'updateTransaction'
            ]),
            getAmount() {
                return this.getUpdateObject.amount
            },
            updateHandler() {
                const transactionDto = {
                    accountId: this.getActiveAccount,
                    TransactionId: this.id,
                    amount: this.amount,
                    isIncoming: this.incoming,
                    transactionDate: new Date(this.date)
                };

                this.updateTransaction(transactionDto);
                this.$emit('closeDialog');
                this.amount = 1000;
                this.incoming = true;
                this.date = new Date().toString();
            }
        },

        watch: {
            show: function () {
                console.log(this.getUpdateObject)
                this.id = this.getUpdateObject.id
                this.amount = this.getUpdateObject.amount
                this.date = this.getUpdateObject.date
                this.incoming = this.getUpdateObject.isIncoming
            },
            AccountId: function (newValue) {
                this.activeAccountId = newValue
            }
        }
  }
</script>