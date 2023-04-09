<template>
    <div class="text-right mb-5 rounded-t-xl rounded-b elevation-2" style="background-color: #ffccd4">
        <v-btn disabled="true" variant="plain">{{ getUser.Username }}</v-btn>
        <v-btn class="ma-2" variant="plain" @click="logoutHandler">Logout</v-btn>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'
    export default {
        data() {
            return {

            }
        },
        computed: {
            ...mapGetters(['getUser'])
        },
        methods: {
            ...mapActions([
                'logout'
            ]),
            logoutHandler() {
                this.logout()
                this.$router.push('/login')
            }
        }, 
        watch: {

        }
    }
</script>