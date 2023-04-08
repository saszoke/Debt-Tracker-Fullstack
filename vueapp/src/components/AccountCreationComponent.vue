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
                        <v-sheet width="300" class="mx-auto">
                            <v-form @submit.prevent v-model="form">
                                <v-text-field label="Name" required :rules="nameRules" v-model="inputName"></v-text-field>
                                <v-file-input prepend-icon="mdi-camera" v-model="file" clearable label="Profile picture" variant="solo" accept="image/*"></v-file-input>
                            </v-form>
                        </v-sheet>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue-darken-1" variant="text" @click="closeDialog">Close</v-btn>
                    <v-btn color="blue-darken-1" variant="text" @click="createHandler" :disabled="!form" v-if="!updateRole">Create</v-btn>
                    <v-btn color="blue-darken-1" variant="text" @click="updateHandler" :disabled="updateFormSubmit" v-else >Update</v-btn>
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
                inputName: null,
                file: null,
                form: false,
                updateForm: true,
                //updateRole: false,
                nameRules: [
                    value => {
                        if (value.trim().length > 0) return true

                        return 'Please enter a valid name'
                    },
                ],
                formData: null
            }
        },

        computed: {
            ...mapGetters([
                'getAccounts',
                'getUpdateId',
                'getAccount'
            ]),
            showModal() {
                return this.accountCreationDialog
            },
            updateFormSubmit() {
                return this.inputName.length < 1
            }
        },
        props: {
            accountCreationDialog: Boolean,
            updateRole: Boolean
        },
        methods: {
            ...mapActions([
                'addAccount',
                'updateAccount',
            ]),
            async createHandler() {
                const formData = new FormData();
                if (this.file) {
                    formData.append('image', this.file[0]);
                } else {
                    formData.append('image', new File([""], "empty"));
                }

                const accountDto = {

                    Name: this.inputName,
                    formData: formData
                };
                this.addAccount(accountDto)
                    .then(() => {
                        this.inputName = "";
                        this.file = null;
                        this.$emit('closeDialog');
                        return
                    })
            },
            async updateHandler() {
                const formData = new FormData();
                if (this.file) {
                    formData.append('image', this.file[0]);
                } else {
                    formData.append('image', new File([""], "empty"));
                }

                const accountDto = {
                    Name: this.inputName,
                    formData: formData
                };
                this.$emit('updateAccount', accountDto);
                this.inputName = "";
                this.file = null;
                this.$emit('closeDialog');
                this.$emit('createRole')
            },
            closeDialog() {
                this.$emit('closeDialog')
                this.$emit('createRole')
                this.inputName = "";
            }
        },

        watch: {
            updateRole(newValue) {
                if (newValue) {
                    this.inputName = this.getAccount(this.getUpdateId).name
                    this.form = true
                    console.log(this.$refs.updateButton)
                    
                }
            },

            accountCreationDialog() {
                console.log(this.updateRole)
                if (this.updateRole) {
                    console.log(this.getAccount(this.getUpdateId))
                    
                }
            },
        }

    }
</script>