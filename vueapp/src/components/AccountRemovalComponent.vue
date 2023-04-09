<template>
    <v-row justify="center">
        <v-dialog v-model="showConfirmDialog" persistent width="auto">
            <v-card>
                <v-card-title class="text-h5">
                    Account removal
                </v-card-title>
                <v-card-text>Would you like to delete the account with all its transactions? Please note once you remove the account it cannot be recovered anymore.</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="green-darken-1" variant="text" @click="disagree">No</v-btn>
                    <v-btn color="green-darken-1" variant="text" @click="agree">Yes</v-btn>
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
              toRemove: null
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
                'removeAccount'
            ]),
            disagree() {
                this.$emit('close')
            },

            agree() {
                this.removeAccount(this.toRemove)
                this.$emit('agreed')
                this.$emit('close')
            }
        },
        watch: {
            toDelete: function () {
                this.toRemove = this.getActiveAccount
            }
        }
  }
</script>