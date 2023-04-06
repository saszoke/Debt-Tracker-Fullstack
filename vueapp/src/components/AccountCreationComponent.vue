<template>
    <div class="text-center">
        <v-dialog v-model="showModal"
                  activator="parent"
                  width="auto"
                  transition="dialog-bottom-transition">
            <v-card>
                <v-card-title>
                    <span class="text-h5">{{ updateRole ? 'Update account' : 'New account' }}</span>
                </v-card-title>
                <v-card-text>
                    <v-container>
                        <v-form @submit.prevent v-model="form">

                            <v-row>
                                <!--<v-col cols="12"
        sm="6"
        md="4">-->
                                <v-text-field label="Name"
                                              required :rules="nameRules" v-model="inputName"></v-text-field>
                                <!--</v-col>-->
                            </v-row>
                        </v-form>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue-darken-1"
                           variant="text"
                           @click="closeDialog">
                        Close
                    </v-btn>
                    <v-btn color="blue-darken-1"
                           variant="text"
                           @click="createHandler"
                           :disabled="!form"
                           v-if="!updateRole">
                        Create
                    </v-btn>
                    <v-btn color="blue-darken-1"
                           variant="text"
                           @click="updateHandler"
                           :disabled="!form"
                           v-else>
                        Update
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex';
    export default {
        data() {
            return {
                //dialog: false
                inputName: null,
                form: false,
                updateRole: false,
                nameRules: [
                    value => {
                        if (value.trim().length > 0) return true

                        return 'Please enter a valid name'
                    },
                ],
            }
        },

        computed: {
            ...mapGetters([
                'getAccounts'
            ]),
            showModal() {
                return this.accountCreationDialog
            }
        },
        props: {
            accountCreationDialog: Boolean,
            updateRole: Boolean
        },
        methods: {
            ...mapActions([
                'addAccount',
                'updateAccount'
            ]),
            createHandler() {
                const accountDto = {
                    Name: this.inputName
                };
                this.addAccount(accountDto)
                    .then(() => {
                        this.inputName = "";
                        this.$emit('closeDialog');
                    })
            },
            updateHandler() {
                const accountDto = {
                    Name: this.inputName
                };
                this.$emit('updateAccount', accountDto);
                this.inputName = "";
                this.$emit('closeDialog');
                this.$emit('createRole')
            },
            closeDialog() {
                this.$emit('closeDialog')
                this.$emit('createRole')
                this.inputName = "";
            }
        }

    }
</script>