<template>
    <v-sheet class="bg-deep-purple pa-12" rounded>
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
                              clearable
                              label="Password"
                              placeholder="Enter your password"></v-text-field>
                <br>

                <v-btn :disabled="!form"
                       :loading="loading"
                       block
                       color="success"
                       size="large"
                       type="submit"
                       variant="elevated">
                    <router-link to="/">Login</router-link>
                    </v-btn>
                </v-form>
                <v-text-field v-model="lambda"
                              :readonly="loading"
                              :rules="[required]"
                              clearable
                              label="Token"
                              placeholder="Enter your password"></v-text-field>
            </v-card>
        </v-sheet>
    </template>

    <script lang="js">
        import { defineComponent } from 'vue';
        import { mapGetters, mapActions } from 'vuex';
        export default defineComponent({
            data() {
                return {
                    loading: false,
                    post: null,
                    form: false,
                    username: null,
                    password: null,
                    token: null,
                    lambda: null
                };
            },
            created() {
                // fetch the data when the view is created and the data is
                // already being observed
                //this.fetchData();
            },
            watch: {
                // call again the method if the route changes
                //'$route': 'fetchData'
            },

            computed: {
                ...mapGetters([
                    'getUser'
                ])
            },
            methods: {

                ...mapActions([
                    'setUser'
                ]),
                onSubmit () {
                    if (!this.form) return

                    this.loading = true
                    this.postLogin()
                  },
                  required (v) {
                    return !!v || 'Field is required'
                  },
                postLogin() {
                    const loginDto = {
                        Username: this.username,
                        Password: this.password
                    }
                    fetch('https://localhost:7092/api/Authentication/Login', {
                        method: "POST", // *GET, POST, PUT, DELETE, etc.
                        mode: "cors", // no-cors, *cors, same-origin
                        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
                        //credentials: "same-origin", // include, *same-origin, omit
                        headers: {
                            "Content-Type": "application/json",
                            // 'Content-Type': 'application/x-www-form-urlencoded',
                        },
                        redirect: "follow", // manual, *follow, error
                        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
                        body: JSON.stringify(loginDto)
                    })
                        .then(r => r.json())
                        .then(json => {
                            console.log(json)
                            this.token = json.token
                            this.$emit('token',this.token)

                            this.setUser({
                                    Username: this.username,
                                    Email: null,
                                    Token: json.token
                                })

                            this.lambda = this.$store.getters.getUser.Username
                            this.password = null
                            this.loading = false
                        })
                }


            },
        });
    </script>
