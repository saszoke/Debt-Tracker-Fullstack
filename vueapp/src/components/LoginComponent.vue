<template>
    <v-sheet class="bg-deep-red pa-12" rounded>
        <v-card class="mx-auto px-6 py-8" max-width="344">
            <v-form v-model="form"
                    @submit.prevent="onSubmit">
                <v-text-field v-model="username"
                              :readonly="loading"
                              :rules="[required]"
                              class="mb-2"
                              clearable
                              label="Username"></v-text-field>

                <v-text-field v-model="password"
                              :readonly="loading"
                              :rules="[required]"
                              type="password"
                              clearable
                              label="Password"
                              placeholder="Enter your password"></v-text-field>
                <br>

                <v-btn :disabled="!form"
                       :loading="loading"
                       block
                       color="#FF8A80"
                       size="large"
                       type="submit"
                       dark
                       variant="elevated">
                    Login
                </v-btn>
            </v-form>
        </v-card>
            <div class="mt-5" v-if="fail">Username or password is incorrect.</div>
        
        </v-sheet>
</template>

    <script lang="js">
        import { defineComponent } from 'vue';
        import { mapGetters, mapActions } from 'vuex';
        export default defineComponent({
            data() {
                return {
                    fail: false,
                    loading: false,
                    post: null,
                    form: false,
                    username: "",
                    password: "",
                    token: null,
                    lambda: null
                };
            },

            computed: {
                ...mapGetters([
                    'getUser'
                ])
            },
            methods: {

                ...mapActions([
                    'setUser',
                    'login'
                ]),
                onSubmit () {
                    if (!this.form) return

                    this.loading = true
                    const loginDto = {
                        Username: this.username,
                        Password: this.password
                    }
                    this.login(loginDto)
                    setTimeout(()=>{
                        this.loading = false
                        this.fail = true
                        this.username = ""
                        this.password = ""
                    },2500)
                  },
                  required (v) {
                    return !!v || 'Field is required'
                  }
            },
            watch: {
                getUser(newValue){
                    this.loading = false
                    this.username = ""
                    this.password = ""
                    if(newValue != null) this.$router.push('/')
                },
                fail(value){
                    if(value){
                        setTimeout(()=>{
                            this.fail = false
                        },2500)
                    }
                }
            }
        });
    </script>
