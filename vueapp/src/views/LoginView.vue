<template>
    <LoginComponent msg="Bank on the Web" @token="storeToken"></LoginComponent>
</template>

<script>
    import LoginComponent from '../components/LoginComponent'
    export default {
        components: {
            LoginComponent
        },
        data() {
            return {

            }
        },
        props: {

        },
        methods: {

        }
    }
</script>