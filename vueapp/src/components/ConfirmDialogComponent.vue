<template>
    <v-row justify="center">
        <v-dialog v-model="showConfirmDialog"
                  persistent
                  width="auto">
            <v-card>
                <v-card-title class="text-h5">
                    Transaction removal
                </v-card-title>
                <v-card-text>Would you like to delete the transaction permanently? Please note that once you remove the the transaction it cannot be recovered anymore.</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="green-darken-1"
                           variant="text"
                           @click="disagree">
                        Cancel
                    </v-btn>
                    <v-btn color="green-darken-1"
                           variant="text"
                           @click="agree">
                        Continue
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex';
    export default {
        props: {
            show: Boolean,
            toDelete: Object
        },
        data () {
          return {
              dialog: false,
              transaction: null
          }
            },
        computed: {
            ...mapGetters([ 'getActiveAccount' ]),
            showConfirmDialog() {
                return this.show
            }
        },
        methods: {
            ...mapActions([
                'removeTransaction'
            ]),
            disagree() {
                this.$emit('close')
            },

            agree() {
                this.removeTransaction(this.transaction)
                this.$emit('close')
            }
        },
        watch: {
            toDelete: function (newValue) {
                this.transaction = newValue
                this.transaction.accountId = this.getActiveAccount
            }
        }
  }
</script>